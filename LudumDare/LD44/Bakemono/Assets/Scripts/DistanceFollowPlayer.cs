using UnityEngine;

public class DistanceFollowPlayer : MonoBehaviour
{
    public float Distance;
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
        UpdateSpeed();
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (_body == null)
            return;

        _body.MovePosition(Vector2.MoveTowards(_body.position, _player.position, _currentSpeed * Time.deltaTime));
    }

    private void UpdateSpeed()
    {
        if (_player.DistanceTo(transform) <= Distance)
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, Speed, Accelleration * Time.deltaTime);
    }
}
