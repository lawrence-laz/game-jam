using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int Health = 1;
    public float MovementSpeed = 2;
    public int HealthWorth = 10;
    public float RushPeriod = 1f;
    public int Damage = 10;

    [Header("Projectiles")]
    public float ProjectilesSpeed;
    public float ProjectilesCount;
    public float ProjectilesAuthoShootPeriod;
}
