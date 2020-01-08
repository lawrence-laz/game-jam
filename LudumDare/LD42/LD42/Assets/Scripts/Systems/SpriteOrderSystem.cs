using UnityEngine;

public class SpriteOrderSystem : MonoBehaviour
{
    private void Update()
    {
        foreach (var sprite in FindObjectsOfType<SpriteRenderer>())
        {
            var hp = sprite.GetComponentInParent<HealthComponent>();

            if (hp != null && hp.IsOutOfBounds == true)
                continue;

            if (sprite.tag == "Cloud")
                continue;

            sprite.sortingOrder = -Mathf.FloorToInt(sprite.transform.position.y * 100);
        }
    }
}
