using DG.Tweening;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    public TurnManager TurnManager { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public Move Move { get; set; }
    public TileObject TargetToFollow { get; set; }
    public Vector2 NextPosition { get; set; }
    public Map Map { get; set; }

    public Sprite[] JumpSprites;
    public Sprite StaticSprite;

    private Sequence _animation;

    private void Start()
    {
        Move = GetComponent<Move>();
        TurnManager = FindObjectOfType<TurnManager>();
        TurnManager.OnTurnStarted.AddListener(OnTurnStarted);
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        Map = FindObjectOfType<Map>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StaticSprite = SpriteRenderer.sprite;

        _animation = DOTween.Sequence();
        foreach (var sprite in JumpSprites)
        {
            _animation = _animation.AppendCallback(() => SpriteRenderer.sprite = sprite)
                .AppendInterval(.1f);
        }
        _animation = _animation.SetLoops(-1, LoopType.Yoyo).Play();
    }

    private void OnTurnStarted()
    {
        if (TargetToFollow == null) return;

        if (Map.Get(NextPosition)?.tag == "Player") return;

        Move.MoveTo(NextPosition);
    }

    private void OnTurnEnded()
    {
        if (TargetToFollow == null) return;

        NextPosition = TargetToFollow.Position;
    }

    public void Follow(TileObject target)
    {
        _animation?.Kill();
        TargetToFollow = target;
        NextPosition = TargetToFollow.Position;
        SpriteRenderer.sprite = StaticSprite;
    }
}
