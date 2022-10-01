using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 1;
    public Rigidbody2D Body;

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Body.velocity = Input.GetAxisRaw("Horizontal") * Speed * Vector2.right;
    }
}
