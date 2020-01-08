using UnityEngine;

public class FaceTowardsMovement : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Vector3 _previousPreviousPosition;

    private void OnEnable()
    {
        _previousPosition = transform.position;
        _previousPreviousPosition = transform.position;
    }

    private void Update()
    {
        UpdateFacingSprite();
    }

    private void UpdateFacingSprite()
    {
        Vector3 facingDirection = transform.position - _previousPreviousPosition;
        _previousPreviousPosition = _previousPosition;
        _previousPosition = transform.position;
        Vector3 scale = transform.localScale;
        scale.x = facingDirection.x < 0 ? 1 : -1;
        transform.localScale = scale;
    }
}
