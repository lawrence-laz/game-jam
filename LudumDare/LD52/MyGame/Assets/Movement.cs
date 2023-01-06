using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MaxSpeed = 1;
    public float Acceleration = 1;
    public Vector2 Direction = Vector2.zero;

    private Rigidbody2D _body;
    private const float MAX_SPEED_SLOW_DOWN_FACTOR = 0.9f;

    private void OnEnable()
    {
        _body = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Direction = Direction.normalized;
        UpdateVelocity();
        UpdateRotationToFaceDirection();
        // UpdateRotationToFaceVelocity();
    }

    private void UpdateRotationToFaceDirection()
    {
        if (_body.velocity.magnitude > Mathf.Epsilon && Direction != Vector2.zero)
        {
            _body.MoveRotation(Quaternion.LookRotation(Direction, Vector3.back));
        }
    }

    private void UpdateRotationToFaceVelocity()
    {
        var direction = _body.velocity.normalized;
        if (_body.velocity.magnitude > Mathf.Epsilon && direction != Vector2.zero)
        {
            _body.MoveRotation(Quaternion.LookRotation(_body.velocity.normalized, Vector3.back));
        }
    }

    private void UpdateVelocity()
    {
        if (_body.velocity.magnitude < MaxSpeed)
        {
            _body.AddForce(Direction * Acceleration);
        }
        else if (_body.velocity.magnitude > MaxSpeed)
        {
            var reducedVelocity = _body.velocity * MAX_SPEED_SLOW_DOWN_FACTOR;
            _body.velocity = reducedVelocity.magnitude < MaxSpeed
                ? _body.velocity.normalized * MaxSpeed
                : reducedVelocity;
        }
    }
}
