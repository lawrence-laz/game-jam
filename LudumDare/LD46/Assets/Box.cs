using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Map Map { get; set; }
    public Move Move { get; set; }

    private void Start()
    {
        Map = FindObjectOfType<Map>();
        Move = GetComponent<Move>();
    }

    public bool TryPush(Vector2 offset)
    {
        var targetPosition = (Vector2)transform.position + offset;
        var targetObjects = Map.GetAll(targetPosition);
        if (Map.IsTraversible(targetPosition) && !targetObjects.Any(x => x.tag == "Enemy"))
        {
            Move.MoveBy(offset);

            return true;
        }

        return false;
    }
}
