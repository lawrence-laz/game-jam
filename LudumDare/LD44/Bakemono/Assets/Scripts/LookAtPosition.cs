using UnityEngine;

public class LookAtPosition : MonoBehaviour
{
    public Vector3 Position;

    private void Update()
    {
        UpdateFacingSprite();
    }

    private void UpdateFacingSprite()
    {
        Vector3 facingDirection = transform.position - Position;
        Vector3 scale = transform.localScale;
        scale.x = facingDirection.x < 0 ? 1 : -1;
        transform.localScale = scale;
    }
}
