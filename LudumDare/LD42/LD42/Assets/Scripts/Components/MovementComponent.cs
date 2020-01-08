using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public float Speed = 0.3f;
    public float SlowDown;
    public float SlowDownRecovery = 0.01f;

    public float ActualSpeed { get { return Mathf.Max(0, Speed - SlowDown); } }

    private void FixedUpdate()
    {
        SlowDown = Mathf.MoveTowards(SlowDown, 0, SlowDownRecovery);
    }
}
