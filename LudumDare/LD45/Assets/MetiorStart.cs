using Libs.Base.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetiorStart : MonoBehaviour
{
    public Vector2 PushForce;
    public float DestroyDistance = 150;

    public Transform Player { get; private set; }

    private void Start()
    {
        var body = GetComponent<Rigidbody>();
        body.AddForceAtPosition(
            new Vector3(
                Random.Range(PushForce.x, PushForce.y),
                Random.Range(PushForce.x, PushForce.y),
                Random.Range(PushForce.x, PushForce.y)), 
            Random.insideUnitSphere / 2);

        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Player.DistanceTo(transform) > DestroyDistance)
            Destroy(gameObject);
    }
}
