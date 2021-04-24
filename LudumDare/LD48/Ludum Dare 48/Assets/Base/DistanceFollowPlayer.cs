using UnityEngine;

public class DistanceFollowPlayer : MonoBehaviour
{
    public float StayWithinDistance;
    public float Speed;
    public float Accelleration;

    private Transform _player;
    private float _currentSpeed = 0;
    private Rigidbody2D _body;

    private void OnEnable()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        UpdateSpeed();
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (_body != null)
        {
            _body.MovePosition(Vector2.MoveTowards(_body.position, _player.position, _currentSpeed * Time.deltaTime));
        }
        else
        {
            var position = transform.position;
            var z = position.z;
            position = Vector2.MoveTowards(transform.position, _player.position, _currentSpeed * Time.deltaTime);
            position.z = z;
            transform.position = position;
        }
    }

    private void UpdateSpeed()
    {
        var playerPosition = (Vector2)_player.position;
        var thisPosition = (Vector2)transform.position;
        var distance = (playerPosition - thisPosition).magnitude;
        _currentSpeed = distance > StayWithinDistance 
            ? Mathf.MoveTowards(_currentSpeed, Mathf.Lerp(0, Speed, Mathf.InverseLerp(StayWithinDistance, StayWithinDistance + 1, distance)) , Accelleration * Time.deltaTime) 
            : 0;
    }
}
