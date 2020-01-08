using UnityEngine;

[RequireComponent(typeof(DeathBehaviour))]
public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float healthCap;

    private ZombieHerbivoreBehaviour zombie;

    public float Health { get { return health; } set { health = value; CheckIfDead(); } }
    public float HealthCap { get { return healthCap; } }

    private void Start()
    {
        health = healthCap;
    }

    private void Update()
    {
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (IsZombie())
            return;

        if (health <= 0)
            GetComponent<DeathBehaviour>().Die();
    }

    private bool IsZombie()
    {
        if (zombie == null)
            zombie = GetComponent<ZombieHerbivoreBehaviour>();

        if (zombie == null || !zombie.HasAlreadyTurned)
            return false;

        return true;
    }
}
