using UnityEngine;

public class EggSkillBehaviour : SkillBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float herbivoreRatio;

    [Header("Herbivore")]
    [SerializeField]
    private GameObject herbivorePrefab;
    [SerializeField]
    private Transform herbivoreContainer;

    [Header("Carnivore")]
    [SerializeField]
    private GameObject carnivorePrefab;
    [SerializeField]
    private Transform carnivoreContainer;

    protected override void ShootLogic(Vector3 position)
    {
        SpawnAnimal(position);
        base.ShootLogic(position);
    }

    private void SpawnAnimal(Vector3 position)
    {
        bool isHerbivore = Random.value < herbivoreRatio;

        Instantiate(isHerbivore ? herbivorePrefab : carnivorePrefab, position, transform.rotation, isHerbivore ? herbivoreContainer : carnivoreContainer);
    }
}
