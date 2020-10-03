using System.Linq;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public GameObject DraggingObject;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && TryGetGameObjectUnderCursor(out var target) && target.CompareTag("Card"))
        {
            DraggingObject = target;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (DraggingObject != null && DraggingObject.TryGetComponent(out Card card) && TryGetGameObjectUnderCursor(out target, ~LayerMask.GetMask("Card")))
            {
                card.PlaceOn(target);
            }

            DraggingObject = default;
        }

        if (DraggingObject)
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                 Input.mousePosition.x,
                 Input.mousePosition.y,
                 DraggingObject.transform.position.z - Camera.main.transform.position.z));

            //Debug.Log(Input.mousePosition);
            var newPosition = new Vector3(
                mouseWorldPosition.x,
                mouseWorldPosition.y,
                DraggingObject.transform.position.z);
            DraggingObject.transform.position = newPosition;
        }
    }

    public static bool TryGetGameObjectUnderCursor(out GameObject gameObject, int layerMask = ~0)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 50, layerMask))
        {
            gameObject = hit.collider.gameObject;
            return true;
        }

        gameObject = default;
        return false;
    }

    public static bool TryGetGameObjectsUnderCursor(out GameObject[] gameObject)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        if (hits?.Length > 0)
        {
            gameObject = hits.Select(x => x.collider.gameObject).ToArray();
            return true;
        }

        gameObject = default;
        return false;
    }
}
