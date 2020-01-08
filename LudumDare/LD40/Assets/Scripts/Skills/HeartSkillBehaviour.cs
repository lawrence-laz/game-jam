using System.Collections.Generic;
using UnityEngine;

public class HeartSkillBehaviour : SkillBehaviour
{
    [SerializeField]
    private AudioSource effectSound;

    private Transform herbivoreContainer;
    private Transform carnivoreContainer;

    public override void OnSelected()
    {
        base.OnSelected();
        GetComponent<SkillManager>().ShootSkill();
    }
    protected override void ShootLogic(Vector3 position)
    {
        ReproduceAll();

        base.ShootLogic(position);
    }

    private void OnEnable()
    {
        herbivoreContainer = GameObject.FindGameObjectWithTag("HerbivoreContainer").transform;
        carnivoreContainer = GameObject.FindGameObjectWithTag("CarnivoreContainer").transform;
    }

    private void ReproduceAll()
    {
        effectSound.Play();

        List<ReproductionBehaviour> animals = new List<ReproductionBehaviour>();
        foreach (Transform animal in herbivoreContainer)
        {
            animals.Add(animal.GetComponent<ReproductionBehaviour>());
        }
        foreach (Transform animal in carnivoreContainer)
        {
            animals.Add(animal.GetComponent<ReproductionBehaviour>());
        }
        foreach (ReproductionBehaviour animal in animals)
        {
            animal.Reproduce();
        }
    }
}
