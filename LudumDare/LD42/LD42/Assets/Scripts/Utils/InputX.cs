using UnityEngine;

public static class InputX
{
    public static Vector3 MouseWorldPosition
    {
        get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
    }

    public static Vector2 GetAxis()
    {
        return Vector2.right * Input.GetAxis("Horizontal") + Vector2.up * Input.GetAxis("Vertical");
    }
}
