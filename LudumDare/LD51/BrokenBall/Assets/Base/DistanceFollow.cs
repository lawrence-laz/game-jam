using UnityEngine;

public class DistanceFollow : MonoBehaviour
{
    public float StayWithinDistance;
    public float Speed;
    public float Accelleration;
    public Vector2 Offset;

    public Transform Target;
    private float _currentSpeed = 0;
    private Rigidbody2D _body;

    private void OnEnable()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (Target == null)
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
            _body.MovePosition(Vector2.MoveTowards(_body.position, Target.position, _currentSpeed * Time.deltaTime));
        }
        else
        {
            var target = Target.position + (Vector3)Offset;
            target.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref _followVelocity, 0.5f, Speed, Time.smoothDeltaTime); //Vector2.MoveTowards(transform.position, _player.position, _currentSpeed * Time.smoothDeltaTime);

            //var target = _player.position;
            //target.z = transform.position.z;
            //transform.DOMove(target, 1);
        }
    }

    private void UpdateSpeed()
    {
        var playerPosition = (Vector2)Target.position;
        var thisPosition = (Vector2)transform.position;
        var distance = (playerPosition - thisPosition).magnitude;
        _currentSpeed = distance > StayWithinDistance 
            ? Mathf.MoveTowards(_currentSpeed, Mathf.Lerp(0, Speed, Mathf.InverseLerp(StayWithinDistance, StayWithinDistance + 1, distance)) , Accelleration * Time.smoothDeltaTime) 
            : 0;
    }
}
