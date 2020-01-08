using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public Vector3 InitialPosition;
    public float Range = 1;
    public float Speed = 0.1f;
    public float Cooldown = 0.05f;
    public float Damage = 35;
    public float LastTimeAttackedAt = 0;
    public float PushPower = 100;
    public float HitDelay = 0.2f;

    public bool IsReady { get { return Time.time >= LastTimeAttackedAt + Cooldown; } }
}
