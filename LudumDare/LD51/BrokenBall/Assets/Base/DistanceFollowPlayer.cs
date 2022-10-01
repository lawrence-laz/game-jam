using UnityEngine;

public class DistanceFollowPlayer : MonoBehaviour
{
    public float StayWithinDistance;
    public float Speed;
    public float Accelleration;
    public Vector2 Offset;

    private Transform _player;
    private float _currentSpeed = 0;
    private Rigidbody2D _body;

    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _body = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (_player == null)
        {
            return;
        }

        UpdateSpeed();
        MoveTowardsPlayer();
    }

    private Vector3 _followVelocity = Vector3.zero;
    private void MoveTowardsPlayer()
    {
        if (_body != null)
        {
            _body.MovePosition(Vector2.MoveTowards(_body.position, _player.position, _currentSpeed * Time.deltaTime));
        }
        else
        {
            var target = _player.position + (Vector3)Offset;
            target.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref _followVelocity, 0.5f, Speed, Time.smoothDeltaTime); //Vector2.MoveTowards(transform.position, _player.position, _currentSpeed * Time.smoothDeltaTime);

            //var target = _player.position;
            //target.z = transform.position.z;
            //transform.DOMove(target, 1);
        }
    }

    private void UpdateSpeed()
    {
        var playerPosition = (Vector2)_player.position;
        var thisPosition = (Vector2)transform.position;
        var distance = (playerPosition - thisPosition).magnitude;
        _currentSpeed = distance > StayWithinDistance 
            ? Mathf.MoveTowards(_currentSpeed, Mathf.Lerp(0, Speed, Mathf.InverseLerp(StayWithinDistance, StayWithinDistance + 1, distance)) , Accelleration * Time.smoothDeltaTime) 
            : 0;
    }
}
