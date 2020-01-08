using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class HungerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float hunger;
    [SerializeField]
    private float hungerCap;
    [SerializeField]
    private float hungerRate;
    [SerializeField]
    [Range(0, 1)]
    private float hungerDamage;

    private HealthBehaviour health;

    public float Hunger { get { return hunger; } set { hunger = Mathf.Clamp(value, 0, HungerCap); } }
    public float HungerCap { get { return hungerCap; } }
    public float HungerDamage { get { return hungerDamage; } set { hungerDamage = value; } }

    private void OnEnable()
    {
        health = GetComponent<HealthBehaviour>();
    }

    private void Start()
    {
        hunger = Random.Range(0.8f * HungerCap, HungerCap);
        InvokeRepeating("GetHungrier", 1, 1);
    }

    private void GetHungrier()
    {
        if (GetComponent<SleepingBehaviour>() != null)
            return;

        hunger -= hungerRate;
        if (hunger < 0)
            hunger = 0;

        if (hunger == 0)
            health.Health -= health.HealthCap * hungerDamage;
    }
}
