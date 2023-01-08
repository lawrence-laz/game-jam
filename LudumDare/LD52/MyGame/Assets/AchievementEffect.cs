using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEffect : MonoBehaviour
{
    private Movement _movement;
    private Transform _target;
    private Rigidbody2D _body;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _target = FindObjectOfType<BookUiController>().transform;
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement.Direction = transform.DirectionTo2D(_target);
        if (transform.DistanceTo2D(_target) < 0.1f)
        {
            _body.velocity = Vector2.zero;
            _movement.Direction = Vector2.zero;
            Destroy(gameObject, 2);
            enabled = false;
        }
    }
}
