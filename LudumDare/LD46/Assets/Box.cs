using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Map Map { get; set; }
    public Move Move { get; set; }
    public TileObject TileObject { get; set; }

    private void Start()
    {
        Map = FindObjectOfType<Map>();
        Move = GetComponent<Move>();
        TileObject = GetComponent<TileObject>();
    }

    public bool TryPush(Vector2 offset)
    {
        var targetPosition = TileObject.Position + offset;
        var targetObjects = Map.GetAll(targetPosition);
        if (Map.IsTraversible(targetPosition, gameObject) && targetObjects.All(x => x.GetComponent<Door>() == null) /*&& !targetObjects.Any(x => x.tag == "Enemy" && x.GetComponent<BombEnemy>() == null)*/)
        {
            Move.MoveBy(offset);

            return true;
        }

        return false;
    }
}
