using UnityEngine;

public class Move : MonoBehaviour
{
    public Map Map { get; set; }

    void Start()
    {
        Map = FindObjectOfType<Map>();
    }

    void Update()
    {
    }

    public void MoveBy(Vector2 offset)
    {
        var newPosition = (Vector2)transform.position + offset;
        transform.position = newPosition;
    }

    public void MoveTo(Vector2 position)
    {
        transform.position = position;
    }

    public bool CanMove(Vector2 offset)
    {
        var newPosition = (Vector2)transform.position + offset;
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
