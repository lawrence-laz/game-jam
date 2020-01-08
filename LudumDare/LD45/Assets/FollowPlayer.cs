using Libs.Base.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float Speed = 2;
    public float StopDistance = 10;

    public Transform Player { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Player.DistanceTo(transform) > StopDistance)
            Rigidbody.MovePosition(transform.position + transform.DirectionTo(Player) * Speed * Time.deltaTime);
    }
}
