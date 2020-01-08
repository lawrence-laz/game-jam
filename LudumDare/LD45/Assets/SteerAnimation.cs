using UnityEngine;

public class SteerAnimation : MonoBehaviour
{
    public float SteerAngle = 50f;
    public float Angle;

    private void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.AngleAxis(SteerAngle * Angle, Vector3.forward), 300 * Time.deltaTime);
    }
}
