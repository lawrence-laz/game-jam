using UnityEngine;

public class CameraLookMouse : MonoBehaviour
{
    public Camera Camera { get; private set; }

    private void Start()
    {
        Camera = Camera.main;   
    }

    private void Update()
    {
        var mouseViewport = Camera.ScreenToViewportPoint(Input.mousePosition);
        var mouseDirection = mouseViewport - Vector3.one / 2;
        var euler = new Vector3(-mouseDirection.y, mouseDirection.x, 0) * 1;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(euler), 0.1f);
    }
}
