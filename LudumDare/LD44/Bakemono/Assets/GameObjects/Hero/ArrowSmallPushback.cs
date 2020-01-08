using UnityEngine;

public class ArrowSmallPushback : MonoBehaviour
{
    private HeroStats _stats;

    private void OnEnable()
    {
        _stats = HeroStats.Get();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PushBackTarget(collision);
    }

    private void PushBackTarget(Collision2D collision)
    {
        if (collision.rigidbody != null)
            collision.rigidbody.AddForce(transform.DirectionTo(collision.collider.transform) * _stats.ArrowPushback);
    }
}
