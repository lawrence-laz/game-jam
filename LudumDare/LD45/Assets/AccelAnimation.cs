using UnityEngine;

public class AccelAnimation : MonoBehaviour
{
    public Vector3 StartHandlePosition;
    public Vector3 EndHandlePosition;

    public ShipControls ShipControls { get; private set; }

    private void Start()
    {
        ShipControls = transform.GetComponentInParent<ShipControls>();
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(StartHandlePosition, EndHandlePosition, (ShipControls.Forward + 1) / 2);
    }
}
