using UnityEngine;

[RequireComponent(typeof(MovingBehaviour))]
[RequireComponent(typeof(LocatorBehaviour))]
[RequireComponent(typeof(EatingBehaviour))]
public class ZombieHerbivoreBehaviour : MonoBehaviour
{
    private const float ZombieSpeed = 0.05f;

    [SerializeField]
    private float eatRange = 1.1f;

    private AgingBehaviour age;
    private HungerBehaviour hunger;
    private MovingBehaviour move;
    private LocatorBehaviour locator;
    private ReproductionBehaviour reproduce;
    private EatingBehaviour eating;
    private Transform carnivores;
    private bool isZombie = false;

    public bool HasAlreadyTurned { get { return isZombie; } }
    public GameObject DiseaseEffect { get; set; }

    public void TurnIntoZombie()
    {
        if (isZombie)
            return;

        isZombie = true;
        tag = "Zombie";
        if (DiseaseEffect != null)
            Destroy(DiseaseEffect);

        HerbivoreBehaviour herbivore = GetComponent<HerbivoreBehaviour>();
        if (herbivore != null)
            Destroy(herbivore);
        age = GetComponent<AgingBehaviour>();
        if (age != null)
            Destroy(age);
        hunger = GetComponent<HungerBehaviour>();
        hunger.HungerDamage = 0;
        reproduce = GetComponent<ReproductionBehaviour>();
        if (reproduce != null)
            Destroy(reproduce);

        locator = GetComponent<LocatorBehaviour>();
        eating = GetComponent<EatingBehaviour>();
        carnivores = GameObject.FindGameObjectWithTag("CarnivoreContainer").transform;

        move = GetComponent<MovingBehaviour>();
        move.Speed = ZombieSpeed;
        move.Flee = false;

        transform.parent = GameObject.FindGameObjectWithTag("ZombieContainer").transform;

        GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Zombie/ZombieSheet_0");
    }

    private void Start()
    {
        InvokeRepeating("ChooseBehaviour", 1, 1 + Random.Range(0, 1));
    }

    private void ChooseBehaviour()
    {
        if (!isZombie)
            return;

        if (GetComponent<SleepingBehaviour>() != null)
            return;

        GameObject target = locator.LocateByTag("Carnivore", eatRange);
        if (target != null)
        {
            eating.Target = target.transform;
        }
        else
        {
            move.TargetTransform = LocatorBehaviour.GetClosest(transform, carnivores);
        }
    }
}
