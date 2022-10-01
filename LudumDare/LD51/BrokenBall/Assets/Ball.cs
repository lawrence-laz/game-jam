using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Body;
    public Vector2 LastVelocity;

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        GetComponent<DistanceFollow>().Target = GameObject.Find("Paddle").transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GetComponent<DistanceFollow>() != null)
        {
            Launch();
        }
        LastVelocity = Body.velocity;
    }

    public void Launch()
    {
        Body.velocity = Vector2.up * Speed;
        FindObjectOfType<Timer>().StartTimer();
        Destroy(GetComponent<DistanceFollow>());
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Body.velocity.normalized.x) > 0.8f)
        {
            var diagonalizedVelocity = Body.velocity.normalized;
            diagonalizedVelocity.y = 1 * Mathf.Sign(diagonalizedVelocity.y);
            diagonalizedVelocity = diagonalizedVelocity.normalized * Body.velocity.magnitude;
            Body.velocity = diagonalizedVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // var normal = Vector2.zero;
        // for(var i = 0; i < other.contactCount; ++i)
        // {
        //     normal -= other.contacts[i].normal;
        // }
        // normal = normal.normalized;

        Vector2 newDirection;
        if (other.gameObject.name == "Paddle")
        {
            newDirection = other.gameObject.transform.DirectionTo(transform);
        }
        else
        {
            var normal = Vector2.zero;
            for (var i = 0; i < other.contactCount; ++i)
            {
                normal += (Vector2)transform.position - other.contacts[i].point;
            }
            normal = normal.normalized;
            newDirection = Vector2.Reflect(LastVelocity, normal).normalized;
        }

        if (Mathf.Abs(newDirection.x) > 0.8f)
        {
            newDirection += new Vector2(0, newDirection.y);
            newDirection = newDirection.normalized;
        }
        Body.velocity = newDirection * Speed;
    }
}
