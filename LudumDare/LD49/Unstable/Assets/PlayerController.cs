using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Transform Horse;
    public RideAnimation Animation;

    [Header("Lean")]
    [Header("----------------------------Balance----------------------------------")]
    public float LeanAcceleration = 35;
    public float LeanMaxSpeed = 120;
    public float LeanCurrentSpeed = 0;
    public float LeaningCurrentRotation = 0;

    [Header("Gravity")]
    public float GravityMaxSpeed = 50;
    public float GravityCurrentSpeed = 0;

    [Header("Falling")]
    public float FallThreshold = 120;
    public UnityEvent OnFell;
    private bool _onFellInvoked;

    [Header("Sideways")]
    [Header("----------------------------Movement----------------------------------")]
    public float SidewaysMaxSpeed = 1;
    public float SidewaysCurrentSpeed = 0;

    [Header("----------------------------Combat----------------------------------")]
    public float RotationMax;
    public float RotationSpeed = 2;
    public HandController Axe;

    private void Update()
    {
        if (Axe.AttackAnimation == null && Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 2f, LayerMask.GetMask("Enemy")))
            {
                //Destroy(hit.collider.gameObject);
                Axe.GetComponent<HandController>().PlayAttackAnimation(hit.point, hit.collider);
            }
            else
            {
                //Axe.GetComponent<HandController>().AttackAnimation(null);
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateLean();
        UpdateGravity();
        UpdateSidewaysMovement(); // Has to go after UpdateGravity.
        UpdateFalling(); // Has to go after UpdateGravity.
    }

    private void UpdateFalling()
    {
        if (!_onFellInvoked && Mathf.Abs(LeaningCurrentRotation) >= FallThreshold)
        {
            _onFellInvoked = true;
            OnFell.Invoke();
        }
    }

    private void UpdateSidewaysMovement()
    {
        if (Animation.JumpAnimation != null)
        {
            return;
        }

        SidewaysCurrentSpeed = Mathf.Lerp(0, SidewaysMaxSpeed, Mathf.Abs(LeaningCurrentRotation) / FallThreshold) * -Mathf.Sign(LeaningCurrentRotation);
        transform.Translate(Vector3.right * SidewaysCurrentSpeed * Time.fixedDeltaTime, Space.World);
        Horse.Translate(Vector3.right * SidewaysCurrentSpeed * Time.fixedDeltaTime, Space.World);
    }

    private void UpdateGravity()
    {
        LeaningCurrentRotation = transform.localEulerAngles.z > 180
            ? transform.localEulerAngles.z - 360
            : transform.localEulerAngles.z;

        var maxSpeed = Mathf.Abs(LeaningCurrentRotation) >= 40
            ? GravityMaxSpeed * 3.5f
            : GravityMaxSpeed;

        var calculatedSpeed = Mathf.Abs(LeaningCurrentRotation) >= 40
            ? LeaningCurrentRotation * 2.75f
            : LeaningCurrentRotation;

        GravityCurrentSpeed = Mathf.Min(Mathf.Abs(calculatedSpeed), maxSpeed) * Mathf.Sign(LeaningCurrentRotation);
        transform.Rotate(GravityCurrentSpeed * Time.fixedDeltaTime * Vector3.forward);
    }

    private void UpdateLean()
    {
        if (Animation.JumpAnimation != null)
        {
            return;
        }

        LeanCurrentSpeed = Mathf.MoveTowards(
            LeanCurrentSpeed,
            LeanMaxSpeed * -Input.GetAxis("Horizontal"),
            LeanAcceleration * Time.fixedDeltaTime);
        transform.Rotate(LeanCurrentSpeed * Time.fixedDeltaTime * Vector3.forward);
    }
}
