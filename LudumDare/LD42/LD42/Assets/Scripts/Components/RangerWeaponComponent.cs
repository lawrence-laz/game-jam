using UnityEngine;

public class RangerWeaponComponent : MonoBehaviour
{
    public GameObject Arrow;
    public float PreShotDuration;
    public float PostShotDuration;
    public bool IsShooting;
    public float LastTimeShot = -100;

    public bool ReadyToShootAgain
    {
        get { return Time.time >= LastTimeShot + PostShotDuration && !IsShooting; }
    }
}
