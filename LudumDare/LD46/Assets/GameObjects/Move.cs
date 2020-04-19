using DG.Tweening;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Map Map { get; set; }
    public TurnManager TurnManager { get; set; }
    public TileObject TileObject { get; set; }

    void Start()
    {
        Map = FindObjectOfType<Map>();
        TurnManager = FindObjectOfType<TurnManager>();
        TileObject = GetComponent<TileObject>();
    }

    void Update()
    {
    }

    public void MoveBy(Vector2 offset)
    {
        var newPosition = TileObject.Position + offset;
        transform.DOMove(newPosition, TurnManager.TurnDuration);
        TileObject.Position = newPosition;
        //transform.position = newPosition;
    }

    public void MoveTo(Vector2 position)
    {
        transform.DOMove(position, TurnManager.TurnDuration);
        TileObject.Position = position;
        //transform.position = position;
    }

    public bool CanMove(Vector2 offset)
    {
        var newPosition = TileObject.Position + offset;
        if (!Map.IsTraversible(newPosition))
        {
            return false; // Do cannot walk here animation
        }

        var objects = Map.GetAll(newPosition);
        if (objects.TryGet<Box>(out var box))
        {
            if (!box.TryPush(offset))
            {
                return false;
            }
        }

        return true;
    }
}
