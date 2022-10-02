using System.Threading;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Libs.Base.Effects;

public class Ball : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Body;
    public Vector2 LastVelocity;
    public ParticleSystem Particles;
    public float PunchStrength = 1;
    public Vector2 VelocitySqueezeScale;

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        GetComponent<DistanceFollow>().Target = GameObject.Find("Paddle").transform;
    }

    private void Update()
    {
        var visuals = transform.Find("Visuals");
        visuals.localScale = Vector2.Lerp(Vector2.one, VelocitySqueezeScale, Mathf.Clamp01(Body.velocity.magnitude / Speed));
        visuals.up = Body.velocity.normalized;
        // var target = (Vector2)transform.position + Body.velocity.normalized;
        // var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && GetComponent<DistanceFollow>() != null)
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

        Particles.Play();

        FindObjectOfType<ScreenShake>().HitShake();

        Vector2 newDirection;
        if (other.gameObject.name == "Paddle")
        {
            GetComponentInChildren<BallAudio>().PlayPaddleHit();
            newDirection = (other.gameObject.transform.DirectionTo(transform) + Vector3.up).normalized;
        }
        else
        {
            if (other.transform.parent?.name == "Boundaries")
            {
                GetComponentInChildren<BallAudio>().PlayBoundaryHit();
            }
            else
            {
                GetComponentInChildren<BallAudio>().PlayTileHit();
                RepeatedStutter.DoIt();
            }

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

        if (other.gameObject.GetComponent<TileDestroyer>() != null)
        {
            Debug.Log("Destroying other: " + other.gameObject.name);
            Destroy(other.gameObject, 0.4f);
            other.gameObject.GetComponentInChildren<Collider2D>().enabled = false;
            var sprite = other.transform.GetComponentInChildren<SpriteRenderer>();
            var color = sprite.color;
            color.a = 0;
            other.gameObject.GetComponentInChildren<SpriteFlash>().Blink(1, out var blinking);
            DOTween.Sequence()
                .Append(blinking)
                .Append(other.transform.DOPunchPosition(
                    punch: transform.DirectionTo(other.transform) * PunchStrength,
                    duration: 0.6f,
                    vibrato: 2,
                    elasticity: 1))
                .Join(sprite.DOColor(color, 0.3f))
                // .Join(other.transform.DOLocalRotate(Vector3.forward * 270, 0.2f, RotateMode.LocalAxisAdd))
                // .Join(other.transform.DOMove(new Vector2(other.transform.position.x, -10), 1))
                .Play();
        }
    }
}
