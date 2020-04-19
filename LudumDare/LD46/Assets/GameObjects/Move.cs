using DG.Tweening;
using UnityEngine;
using Libs.Base.Extensions;

public class Move : MonoBehaviour
{
    public Map Map { get; set; }
    public TurnManager TurnManager { get; set; }
    public TileObject TileObject { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    private Sequence _animation;

    void Start()
    {
        Map = FindObjectOfType<Map>();
        TurnManager = FindObjectOfType<TurnManager>();
        TileObject = GetComponent<TileObject>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
    }

    private void Animate()
    {
        _animation?.Kill();
        transform.localScale = Vector3.one;
        _animation = DOTween.Sequence()
            .Append(transform.DOBlendableScaleBy(Vector3.down * 0.15f, TurnManager.TurnDuration / 2))
            .Append(transform.DOBlendableScaleBy(Vector3.up * 0.15f, TurnManager.TurnDuration / 2))
            .Play();
    }

    private void OnDestroy()
    {
        _animation?.Kill();
    }

    public void MoveBy(Vector2 offset)
    {
        var newPosition = TileObject.Position + offset;
        UpdateSpriteDirection(offset);
        transform.DOMove(newPosition, TurnManager.TurnDuration);
        Animate();
        TileObject.Position = newPosition;
        //transform.position = newPosition;
    }

    public void MoveTo(Vector2 position)
    {
        var offset = position - (Vector2)transform.position;
        UpdateSpriteDirection(offset);

        transform.DOMove(position, TurnManager.TurnDuration);
        Animate();
        TileObject.Position = position;
        //transform.position = position;
    }

    public void UpdateSpriteDirection(Vector2 offset)
    {
        if (GetComponent<Box>() != null)
        {
            return;
        }

        if (offset.x != 0)
        {
            SpriteRenderer.transform.SetScaleX(offset.x > 0 ? 1f : -1f);
        }
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

        UpdateSpriteDirection(offset);

        return true;
    }
}
