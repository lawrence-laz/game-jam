using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ControlledObject;

    private Vector2 _axisInput;

    private void Update()
    {
        _axisInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        ControlMovement();    
    }

    private void ControlMovement()
    {
        var movement = ControlledObject.GetComponent<Movement>();
        movement.Direction = _axisInput;
    }
}
