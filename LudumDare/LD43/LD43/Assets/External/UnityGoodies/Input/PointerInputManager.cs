using UnityEngine;
using UnityGoodies;

public class PointerInputManager : MonoBehaviour
{
    private const string OnMouseDownAll = "OnMouseDownAll";
    private const string OnMouseUpAll = "OnMouseUpAll";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var objects = Physics2DExtended.RaycastAllAtMousePosition();
            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    obj.collider.gameObject.SendMessage(OnMouseDownAll, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            var objects = Physics2DExtended.RaycastAllAtMousePosition();
            if (objects != null)
            {

                foreach (var obj in Physics2DExtended.RaycastAllAtMousePosition())
                {
                    obj.collider.gameObject.SendMessage(OnMouseUpAll, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
