using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBall : MonoBehaviour
{
    public Rigidbody2D Body;
    public Transform Paddle;

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Paddle = FindObjectOfType<Paddle>().transform;
    }

    void LateUpdate()
    {
        if (Body.velocity.y < 0)
        {
            // Going down, start homing.
            Body.velocity = Vector2.MoveTowards(Body.velocity, transform.DirectionTo(Paddle) * Mathf.Max(Body.velocity.magnitude, 5), Time.deltaTime * 30);
        }
    }
}
