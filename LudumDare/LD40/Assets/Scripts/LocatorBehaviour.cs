using UnityEngine;

public class LocatorBehaviour : MonoBehaviour
{
    public GameObject LocateByTag(string tag, float radius)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject == gameObject) // Exclude self
                continue;

            if (hit.collider.tag == tag)
                return hit.collider.gameObject;
        }

        return null;
    }

    public static Transform GetClosest (Transform pivot, Transform choices)
    {
        float minDistance = float.MaxValue;
        Transform closestChoice = null;

        foreach (Transform choice in choices)
        {
            if (choice == pivot)
                continue;

            float distance = (pivot.position - choice.position).magnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestChoice = choice;
            }
        }

        return closestChoice;
    }
}
