using UnityEngine;

public class SleepSkillBehaviour : SkillBehaviour
{
    [SerializeField]
    private float duration = 10;
    [SerializeField]
    private GameObject zzzEffectPrefab;

    protected override void ShootLogic(Vector3 position)
    {
        SleepTargets(position);
        base.ShootLogic(position);
    }

    private void SleepTargets(Vector3 position)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(position, radius*2, Vector3.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.tag == "Plant_Food")
                continue;

            SleepingBehaviour sleep = hit.collider.gameObject.AddComponent<SleepingBehaviour>();
            GameObject zzzObject = Instantiate(zzzEffectPrefab, hit.collider.transform.position, transform.rotation, hit.collider.transform);
            sleep.ZzzEffect = zzzObject;
            sleep.SleepFor(duration);
        }
    }
}