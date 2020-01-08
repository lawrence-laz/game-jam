using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private void Update()
    {
        UpdateFacingSprite();
    }

    private void UpdateFacingSprite()
    {
        Vector3 facingDirection = InputX.MouseWorldPosition - transform.position;
        Vector3 scale = transform.localScale;
        scale.x = facingDirection.x < 0 ? 1 : -1;
        transform.localScale = scale;
    }
}
