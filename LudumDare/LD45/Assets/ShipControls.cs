using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    public float ForwardSpeed = 1;
    public float SideRotationPower = 60;
    public float SideRotationSpeed = 40;
    public float SideSpeed = 2;
    public float UpSpeed = 2;
    public float Forward;
    public Vector3 Velocity;
    public Quaternion Rotation;

    public SteerAnimation SteerAnimation { get; private set; }

    private void Start()
    {
        SteerAnimation = GameObject.FindObjectOfType<SteerAnimation>();
    }

    private void Update()
    {
        Forward = Input.GetKey(KeyCode.W)
            ? 1
            : Input.GetKey(KeyCode.S)
            ? -1
            : 0;

        var sideAngle = Input.GetKey(KeyCode.A)
            ? 1
            : Input.GetKey(KeyCode.D)
            ? -1
            : 0;

        SteerAnimation.Angle = sideAngle;

        var upTargetVelocity = Vector3.up * (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E)
            ? UpSpeed
            : 0);
        var forwardTargetVelocity = transform.forward * Forward * ForwardSpeed;
        var horizontalVel = (-transform.right) * SideSpeed * sideAngle;
        horizontalVel.y = 0;
        var targetVelocity = upTargetVelocity + forwardTargetVelocity + horizontalVel;
        Velocity = Vector3.MoveTowards(Velocity, targetVelocity, 5 * Time.deltaTime);

        //Debug.Log(Velocity.sqrMagnitude);

        transform.position += Velocity * Time.deltaTime;
        Rotation = Quaternion.RotateTowards(Rotation, Quaternion.AngleAxis(SideRotationPower * sideAngle, Vector3.forward),
            SideRotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
