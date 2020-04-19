using DG.Tweening;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Map Map { get; set; }
    public TurnManager TurnManager { get; set; }
    public TileObject TileObject { get; set; }

    private Sequence _animation;

    void Start()
    {
        Map = FindObjectOfType<Map>();
        TurnManager = FindObjectOfType<TurnManager>();
        TileObject = GetComponent<TileObject>();
    }

    void Update()
    {
    }

    private void Animate()
    {
        _animation?.Kill();
        _animation = DOTween.Sequence()
            .Append(transform.DOBlendableScaleBy(Vector3.down * 0.5f, TurnManager.TurnDuration / 2))
            .Append(transform.DOBlendableScaleBy(Vector3.up * 0.5f, TurnManager.TurnDuration / 2))
            .Play();
    }

    private void OnDestroy()
    {
        _animation?.Kill();
    }

    public void MoveBy(Vector2 offset)
    {
        var newPosition = TileObject.Position + offset;
        transform.DOMove(newPosition, TurnManager.TurnDuration);
        Animate();
        TileObject.Position = newPosition;
        //transform.position = newPosition;
    }

    public void MoveTo(Vector2 position)
    {
        transform.DOMove(position, TurnManager.TurnDuration);
        Animate();
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
