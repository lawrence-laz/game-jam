using UnityEngine;

[RequireComponent(typeof(HungerBehaviour))]
public class EatingBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float hungerIncrease;
    [SerializeField]
    private float eatRate;

    private HungerBehaviour hunger;
    private Animator animator;

    public Transform Target { get { return target; } set { target = value; } }

    private void OnEnable()
    {
        hunger = GetComponent<HungerBehaviour>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InvokeRepeating("Eat", 1, eatRate);
    }

    private void Eat()
    {
        if (target == null || GetComponent<SleepingBehaviour>() != null)
        {
            animator.SetBool("IsAttacking", false);
            return;
        }

        HealthBehaviour health = target.GetComponent<HealthBehaviour>();
        if (health == null || health.Health <= 0)
        {
            animator.SetBool("IsAttacking", false);
            return;
        }
        animator.SetBool("IsAttacking", true);

        health.Health -= damage;
        hunger.Hunger += hungerIncrease;
    }
}