using UnityEngine;

namespace Libs.Base.Input
{
    public static class InputX
    {
        public static Vector3 MouseWorldPosition
        {
            get { return Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition); }
        }

        public static Vector2 GetAxis()
        {
            return Vector2.right * UnityEngine.Input.GetAxis("Horizontal") + Vector2.up * UnityEngine.Input.GetAxis("Vertical");
        }
    }
}
