using Libs.Base.Extensions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ShotBy;
    public float Damage = 2;
    public GameObject ExplosionFx;
    public GameObject SimpleHitFx;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (ShotBy.DoesShareHierarchy(collision.gameObject))
        {
            return;
        }

        if (collision.TryGetComponent<Health>(out var health)
            || collision.gameObject.TryGetComponentInParent(out health))
        {
            health.CurrentValue -= Damage;
            if (health.CurrentValue <= 0)
            {
                Explode(health.transform.position);
                if (!health.gameObject.CompareTag("Player"))
                {
                    Destroy(health.gameObject);
                }
            }
        }

        if (collision.TryGetComponent<Asteroid>(out var asteroid)
            || collision.gameObject.TryGetComponentInParent(out asteroid))
        {
            asteroid.CurrentValue -= Damage;
            if (asteroid.CurrentValue <= 0)
            {
                Explode(asteroid.transform.position);
                asteroid.BigExposion = true;
            }
        }

        Destroy(gameObject);
        HitEffect(transform.position);
    }
    private void Explode(Vector3 position)
    {
        Instantiate(ExplosionFx, position, Quaternion.identity);
    }

    private void HitEffect(Vector3 position)
    {
        if (SimpleHitFx == null)
        {
            return;
        }

        Instantiate(SimpleHitFx, position, Quaternion.identity);
    }
}
