using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public ShipControls ShipControls { get; private set; }
    public Text Text { get; private set; }
    public CameraShake CameraShake { get; private set; }

    private int maxSpeed = 500;

    private void Start()
    {
        ShipControls = GetComponentInParent<ShipControls>();
        Text = GetComponentInChildren<Text>();
        CameraShake = FindObjectOfType<CameraShake>();
    }

    private void Update()
    {
        var speed = ((int)(ShipControls.Velocity.sqrMagnitude * 100 * 1.1f));
        if (speed > maxSpeed)
            maxSpeed = speed;

        if (speed + 23 >= maxSpeed * 0.5f)
        {
            CameraShake.VerySligthShake();
        }


        Text.text = speed.ToString();
    }
}
