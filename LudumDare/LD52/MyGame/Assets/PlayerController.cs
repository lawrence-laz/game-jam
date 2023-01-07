using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ControlledObject;
    public Interactor Interactor;

    private Vector2 _axisInput;

    private void Update()
    {
        _axisInput = new Vector2(
            (Input.GetAxisRaw("Horizontal") + Input.GetAxis("Horizontal")) / 2,
            (Input.GetAxisRaw("Vertical") + Input.GetAxis("Vertical")) / 2
        );

        ControlMovement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
        {
            if (!Interactor.TryInteract())
            {
            }
        }
        else if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Q))
        {
            Interactor.GetComponentInChildren<Holder>().TryDropAll();
        }
    }

    private void ControlMovement()
    {
        var movement = ControlledObject.GetComponent<Movement>();
        movement.Direction = Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0
            ? Vector2.zero
            : movement.Direction = _axisInput;
    }
}
