using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private const float MOUSE_Z_POSITION = 5;

    private void Update()
    {
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = MOUSE_Z_POSITION;
        transform.position = mouseWorldPosition;
    }
}
