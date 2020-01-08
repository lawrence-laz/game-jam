using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private const float BottomPadding = 0.02f;

    [SerializeField]
    private float triggerZone = 0.1f;
    [SerializeField]
    private float speed = 1;

    private void Update()
    {
        UpdatePosition();
        UpdateZoom();
    }

    private void UpdatePosition()
    {
        Vector3 newPosition = transform.position + GetMoveDirection() * speed;

        if (newPosition.x < 46 || newPosition.y < 46 || newPosition.y > 75 || newPosition.x > 100)
            return;

        transform.position = newPosition;
    }

    private void UpdateZoom()
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + Input.mouseScrollDelta.y, 10, 25);
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = Vector3.zero;

        if (mousePosition.x > 1.2f || mousePosition.y > 1.2f || mousePosition.x < -0.2f || mousePosition.y < -0.2f)
            return direction;

        if (mousePosition.x + triggerZone >= 1 || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            direction.x = 1;
        else if (mousePosition.x - triggerZone < 0 || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            direction.x = -1;

        if (mousePosition.y + triggerZone >= 1 || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            direction.y = 1;
        else if (mousePosition.y - triggerZone + BottomPadding < 0 || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            direction.y = -1;

        return direction.normalized;
    }
}
