using DG.Tweening;
using UnityEngine;

public class OutOfBoundsSystem : Singleton<OutOfBoundsSystem>
{
    public void HandleOutOfBounds(HealthComponent health)
    {
        if (health == null)
            return;

        Debug.Log(health.gameObject.name + " out of bounds.");
        health.Health = 0;
        health.IsOutOfBounds = true;

        foreach (var collider in health.GetComponentsInChildren<Collider2D>())
        {
            collider.enabled = false;
        }

        foreach (var particle in health.GetComponentsInChildren<ParticleSystem>())
        {
            particle.Stop();
        }

        FallBody(health.GetComponent<Rigidbody2D>());
        SetRenderOrdering(health.GetComponentsInChildren<SpriteRenderer>());
    }

    internal void HandleOutOfBoundsItem(Transform obj)
    {
        Debug.Log(obj.gameObject.name + " out of bounds.");

        obj.DOMoveY(-20, 2);

        SetRenderOrdering(obj.GetComponents<SpriteRenderer>());
    }

    private void SetRenderOrdering(SpriteRenderer[] spriteRenderers)
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.sortingOrder += 10000 * (renderer.transform.position.y > 0 ? -1 : 1);
        }
    }

    private void FallBody(Rigidbody2D body)
    {
        if (body == null)
            return;

        body.gravityScale = 10;
    }
}
