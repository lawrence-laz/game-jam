using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// No acceleration, just constant speed?

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float MaxSpeed = 1;
    public float Acceleration = 0.1f;

    Vector3 velocity;
    float currentSpeed = 0;

    CharacterController controller;
    TextManager textManager;

    #region Mouse
    Vector3 mousePositionLastFrame = Vector2.zero;
    private Vector2 mousePositionDelta { get { return Input.mousePosition - mousePositionLastFrame; } }
    private Vector2 mousePositionDeltaInversed { get { return mousePositionLastFrame - Input.mousePosition; } }
    [SerializeField]
    [Range(1, 10)]
    private float mouseSensitivity;
#if UNITY_EDITOR
    bool controlsEnabled = true;
#endif
    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        textManager = TextManager.Instance;

        mousePositionLastFrame = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        UpdateRotation();
        UpdateVelocity();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
            controlsEnabled = !controlsEnabled;
#endif
    }

    private void LateUpdate()
    {
        mousePositionLastFrame = Input.mousePosition;
    }

    private void UpdateVelocity()
    {
        Vector3 forwardVelocity = transform.forward * Input.GetAxis("Vertical") * currentSpeed;
        Vector3 sidewaysVelocity = Quaternion.AngleAxis(90, Vector3.up) * transform.forward * Input.GetAxis("Horizontal") *  currentSpeed;
        velocity = forwardVelocity + sidewaysVelocity;
    }

    private void UpdateRotation()
    {
#if UNITY_EDITOR
        if (!controlsEnabled)
            return;
#endif

        Quaternion rotation = transform.rotation;
        Vector3 eulerRotation = transform.rotation.eulerAngles + new Vector3(-Input.GetAxis("Mouse Y")/*mousePositionDeltaInversed.y*/ * mouseSensitivity, /*mousePositionDeltaInversed.x*/Input.GetAxis("Mouse X") * mouseSensitivity, 0);
        float clamped = eulerRotation.x;
        if (/*mousePositionDeltaInversed.y*/-Input.GetAxis("Mouse Y") < 0 && clamped < 310 && clamped > 70)
            clamped = 310;
        else if (/*mousePositionDeltaInversed.y*/-Input.GetAxis("Mouse Y") > 0 && clamped > 70 && clamped < 310)
            clamped = 70;
        eulerRotation.x = clamped;
        rotation.eulerAngles = eulerRotation;
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {
        if (!textManager.AllowMovement)
            return;

        currentSpeed = Mathf.MoveTowards(currentSpeed, MaxSpeed, Acceleration);
        controller.SimpleMove(velocity);
    }
}
