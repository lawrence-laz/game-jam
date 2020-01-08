using UnityEngine;

public class PlayerMovementSystem : Singleton<PlayerMovementSystem>
{
    private GameObject _gameObject;
    //private Transform _transform;
    private Rigidbody2D _body;
    private MovementComponent _movement;
    private HealthComponent _health;

    private void Start()
    {
        _gameObject = GameObject.FindGameObjectWithTag("Player");
        _health = _gameObject.GetComponent<HealthComponent>();
        //_transform = _gameObject.transform;
        _body = _gameObject.GetComponent<Rigidbody2D>();
        _movement = _gameObject.GetComponent<MovementComponent>();
    }

    private void FixedUpdate()
    {
        if (_health.Health <= 0)
            return;

        _body.AddForce(InputX.GetAxis() * _movement.Speed * 4.5f * (_body.velocity.magnitude < 5 ? 3.5f : 1), ForceMode2D.Impulse);
        //_body.MovePosition(_body.position + InputX.GetAxis() * _movement.ActualSpeed);
    }
}
