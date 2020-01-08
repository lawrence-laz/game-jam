using UnityEngine;

public class ArrowFly : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _body;

    private void OnEnable()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveToLocalUpDirectionBySpeed();
    }

    private void MoveToLocalUpDirectionBySpeed()
    {
        _body.MovePosition(_body.position + (Vector2)(Time.deltaTime * Speed * transform.up));
    }
}
