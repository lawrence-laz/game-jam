using Libs.Base.Extensions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ShotBy;
    public float Damage = 30;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShotBy.DoesShareHierarchy(collision.collider.gameObject))
        {
            return;
        }

        if (collision.otherCollider.TryGetComponent<Health>(out var health)
            || collision.otherCollider.gameObject.TryGetComponentInParent(out health))
        {
            health.CurrentValue -= Damage;
        }

        Destroy(gameObject);
    }
}
