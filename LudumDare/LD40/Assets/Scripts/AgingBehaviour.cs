using UnityEngine;

[RequireComponent(typeof(DeathBehaviour))]
public class AgingBehaviour : MonoBehaviour
{
    [SerializeField]
    private float age = 0;
    [SerializeField]
    private float ageCap = 5;

    private ZombieHerbivoreBehaviour zombie;

    public float Age { get { return age; } }

    private void Start()
    {
        ageCap = ageCap * Random.Range(1.1f, 1.5f);
    }

    private void Update()
    {
        IncreaseAge();
        CheckForAgeCap();
    }

    private void IncreaseAge()
    {
        age += Time.deltaTime;
    }

    private void CheckForAgeCap()
    {
        if (IsZombie())
            return;

        if (age >= ageCap)
        {
            GetComponent<DeathBehaviour>().Die();
        }
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
