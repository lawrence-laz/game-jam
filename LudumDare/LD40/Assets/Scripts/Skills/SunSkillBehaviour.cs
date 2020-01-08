using System.Collections.Generic;
using UnityEngine;

public class SunSkillBehaviour : SkillBehaviour
{
    [SerializeField]
    private int additionalPlantsToSpawn;

    private Transform plantContainer;
    private SpawnerBehaviour spawner;

    public override void OnSelected()
    {
        base.OnSelected();
        GetComponent<SkillManager>().ShootSkill();
    }

    protected override void ShootLogic(Vector3 position)
    {
        GrowAllPlants();
        spawner.SpawnSomePlants(additionalPlantsToSpawn);

        base.ShootLogic(position);
    }

    private void OnEnable()
    {
        plantContainer = GameObject.FindGameObjectWithTag("PlantContainer").transform;
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerBehaviour>();
    }

    private void GrowAllPlants()
    {
        // Two loops to avoid infinite growth.
        List<ReproductionBehaviour> plants = new List<ReproductionBehaviour>();
        foreach (Transform plant in plantContainer)
        {
            plants.Add(plant.GetComponent<ReproductionBehaviour>());
        }
        foreach (ReproductionBehaviour plant in plants)
        {
            plant.Reproduce();
        }
    }
}
