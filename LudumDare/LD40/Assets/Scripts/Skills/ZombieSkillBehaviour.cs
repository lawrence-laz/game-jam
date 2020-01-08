using UnityEngine;

public class ZombieSkillBehaviour : SkillBehaviour
{
    [SerializeField]
    private GameObject diseaseEffectPrefab;

    protected override void ShootLogic(Vector3 position)
    {
        ShootZombieDisease(position);

        base.ShootLogic(position);
    }

    private void ShootZombieDisease(Vector3 position)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, radius * 2, Vector3.zero);
        foreach (RaycastHit2D hit in hits)
        {
            HerbivoreBehaviour herbivore = hit.collider.GetComponent<HerbivoreBehaviour>();
            if (herbivore == null || herbivore.GetComponent<ZombieHerbivoreBehaviour>() != null)
                continue;

            ZombieHerbivoreBehaviour zombie = herbivore.gameObject.AddComponent<ZombieHerbivoreBehaviour>();
            GameObject diseaseEffect = Instantiate(diseaseEffectPrefab, hit.collider.transform.position, transform.rotation, hit.collider.transform);
            zombie.DiseaseEffect = diseaseEffect;
        }
    }
}
