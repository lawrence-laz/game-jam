using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public enum RushState { Rest, Aim, Rush }

public class RushToHero : MonoBehaviour
{
    public UnityEvent OnRest;
    public UnityEvent OnAiming;
    public UnityEvent OnRushing;
    public GameObject TargetMarkerPrefab;

    private EnemyStats _stats;
    private RushState _state = RushState.Rest;
    private float _startedRest = 0;
    private Vector3 _target;
    private Rigidbody2D _body;
    private LookAtPosition _lookAt;
    private Transform _player;
    private Transform _targetMarker;
    private float _aimingProgress = 0;

    private void OnEnable()
    {
        _stats = GetComponent<EnemyStats>();
        _body = GetComponent<Rigidbody2D>();
        _lookAt = GetComponent<LookAtPosition>();
        _player = GameObject.FindWithTag("Player").transform;
        OnRest.Invoke();
    }

    private void Update()
    {
        FromRestToAim();
        Aimlogic();
        RushLogic();
    }

    private void Aimlogic()
    {
        if (_state == RushState.Aim)
        {
            _lookAt.Position = _player.position;
            _targetMarker.position = Vector3.MoveTowards(_targetMarker.position, _player.position, 
                _aimingProgress < 0.8f
                ? HeroStats.Get().MovementSpeed * _aimingProgress * 0.5f * Time.deltaTime
                :(_aimingProgress < 0.93f)
                    ? HeroStats.Get().MovementSpeed * 1.2f * Time.deltaTime
                    : HeroStats.Get().MovementSpeed * 2f * Time.deltaTime);
        }
    }

    private void RushLogic()
    {
        if (_state == RushState.Rush)
        {
            _lookAt.enabled = false;
               var nextPosition = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _stats.MovementSpeed);
            _lookAt.Position = _target;

            if (nextPosition == transform.position)
            {
                _state = RushState.Rest;
                _startedRest = Time.time;
                Destroy(_targetMarker.gameObject);
                OnRest.Invoke();
            }
            else
            {
                _body.MovePosition(nextPosition);
                transform.LookAt2D(_target);
            }
        }
    }

    private void FromRestToAim()
    {
        if (_state != RushState.Rest)
            return;



        transform.rotation = Quaternion.identity;

        if (Time.time - _startedRest > _stats.RushPeriod)
        {
            if (_targetMarker == null)
                _targetMarker = Instantiate(TargetMarkerPrefab, _player.position, Quaternion.identity).transform;

            _lookAt.enabled = true;
            _state = RushState.Aim;
            OnAiming.Invoke();

            DOTween.Sequence()
                .Append(DOTween.To(
                    () => _aimingProgress, 
                    (value) => _aimingProgress = value, 
                    1, 1))
                .AppendCallback(() =>
                {
                    _aimingProgress = 0;
                    _target = _player.position;
                    _state = RushState.Rush;
                    OnRushing.Invoke();
                });
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_state == RushState.Rush && collision.collider.gameObject.layer != LayerMask.NameToLayer("Hero Arrow"))
        {
            _state = RushState.Rest;
            OnRest.Invoke();
            _startedRest = Time.time;
            if (_targetMarker != null)
            Destroy(_targetMarker.gameObject);
        }
    }

    private void OnDisable()
    {
        if (_targetMarker != null)
            Destroy(_targetMarker.gameObject);
    }

    private void OnDestroy()
    {
        if (_targetMarker != null)
            Destroy(_targetMarker.gameObject);
    }
}
