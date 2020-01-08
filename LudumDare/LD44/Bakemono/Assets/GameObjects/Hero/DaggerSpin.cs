using UnityEngine;

public class DaggerSpin : MonoBehaviour
{
    public float Speed;

    Rigidbody2D _body;

    private void OnEnable()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _body.MoveRotation(_body.rotation += Speed * Time.fixedDeltaTime);
        //transform.Rotate(0, 0, );
    }
}
