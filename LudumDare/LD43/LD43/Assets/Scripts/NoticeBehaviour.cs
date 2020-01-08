using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityGoodies;

public class NoticeBehaviour : MonoBehaviour
{
    public float Distance = 3;
    public float FieldOfView = 110;
    public LayerMask Mask;
    public bool HasNoticed = false;
    public bool InSight = false;
    public Vector3 LastSightPosition = Vector3.zero;
    public float LastSightTime = 0;
    public float TimeToForget = 7;
    public bool SecondaryAttraction;
    public Vector3 SecondaryAttractionPosition;

    private Transform _player;
    private Transform _head;
    private float _secondaryAttractionStart;
    private float _secondaryAttractionDuration = 5;

    public bool JustInSight
    {
        get { return Time.time < LastSightTime + 4.2f; }
    }

    public bool IsInLineOfSight
    {
        get
        {
            if (transform.position.DistanceTo(_player.position) > Distance)
            {
                //Debug.LogFormat("{0} is now <color=green>NOT IN RADIUS</color> for {1} to notice him.", _player.gameObject.name, gameObject.name);
                return false;
            }
            //Debug.LogFormat("{0} is now <color=red>IN RADIUS</color> for {1} to notice him.", _player.gameObject.name, gameObject.name);

            RaycastHit hit;
            if (!Physics.Raycast(_head.position, _head.position.DirectionTo(Camera.main.transform.position), out hit, Distance, Mask))
            {
                //Debug.LogFormat("{0} is hidden", _player.gameObject.name);
                return false;
            }
            if (hit.transform.GetRoot().tag != "Player")
            {
                //Debug.LogFormat("{0} <color=green>IS COVERED</color> behind {1}", _player.gameObject.name, hit.transform.gameObject.name);
                return false;
            }
            //Debug.LogFormat("{0} is now <color=red>NOT COVERED</color>  for {1} to notice him.", _player.gameObject.name, gameObject.name);

            var toPlayerVector = _head.position.DirectionTo(_player.position);
            if (Vector3.Angle(toPlayerVector, _head.forward) > FieldOfView * .5f)
            {
                //Debug.LogFormat("{0} <color=green>IS BEHIND</color> {1}, so he cannot be seen.", _player.gameObject.name, gameObject.name);
                return false;
            }

            //Debug.LogFormat("{0} <color=red>IS SEEN !!!</color> by {1}", _player.gameObject.name, gameObject.name);

            LastSightPosition = Camera.main.transform.position + Vector3.down * .5f;//_player.position;
            LastSightTime = Time.time;

            return true;
        }
    }

    public Transform Player { get { return _player; } }

    public UnityEvent OnNoticed;
    public UnityEvent OnLost;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _head = transform.Find("Head");

        StartCoroutine(TryToNotice());
    }

    public void CheckSound(Vector3 position)
    {
        SecondaryAttraction = true;
        SecondaryAttractionPosition = position;
        _secondaryAttractionStart = Time.time;
        this.ForAllComponents<NavMeshAgent>(nav =>
        {
            if (nav.enabled)
            {
                nav.SetDestination(position);
            }
        });
    }

    private void Update()
    {
        if (HasNoticed && Time.time > LastSightTime + TimeToForget)
        {
            HasNoticed = false;
            OnLost.Invoke();
        }

        if (SecondaryAttraction && Time.time > _secondaryAttractionStart + _secondaryAttractionDuration)
        {
            SecondaryAttraction = false;
        }
    }

    // Should try to notice every once in a while to allow for quick sneaks
    private IEnumerator TryToNotice()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);

            if (!enabled)
            {
                continue;
            }

            //if (HasNoticed)
            //{
            //    continue;
            //}

            if (!IsInLineOfSight)
            {
                continue;
            }

            if (!HasNoticed)
            {
                HasNoticed = true;
                OnNoticed.Invoke();
            }
        }
    }
}
