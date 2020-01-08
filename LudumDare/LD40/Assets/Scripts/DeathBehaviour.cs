using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject cadaverPrefab;

    public void Die()
    {
        ZombieHerbivoreBehaviour zombie = GetComponent<ZombieHerbivoreBehaviour>();
        if (zombie != null && !zombie.HasAlreadyTurned)
        {
            zombie.TurnIntoZombie();
            return;
        }

        if (cadaverPrefab != null)
        {
            Instantiate(cadaverPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
