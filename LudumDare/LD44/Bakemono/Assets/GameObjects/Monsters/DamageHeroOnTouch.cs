using DG.Tweening;
using UnityEngine;

public class DamageHeroOnTouch : MonoBehaviour
{
    public bool OnlyOnce = false;

    private EnemyStats _stats;

    private void OnEnable()
    {
        _stats = GetComponent<EnemyStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamagePlayerLogic(collision);
    }

    private void DamagePlayerLogic(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
            return;


        collision.collider.GetComponent<HeroStats>().Health -= _stats.Damage;

        if (OnlyOnce)
            Destroy(gameObject);
    }
}
