using DG.Tweening;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public TurnManager TurnManager { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    public Sprite[] OnSprites;
    public Sprite OffSprite;
    public Sprite ReadySprite;
    public Sprite AlmostReadySprite;

    public bool On;
    public int Ticks;
    public const int Period = 3;

    private const int _ticksBeforeActivating = Period - 1;
    private Sequence _onAnimation;

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        TurnManager = FindObjectOfType<TurnManager>();
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        On = Ticks % Period == 0 ? true : false;
        UpdateSprite();
    }

    private void OnTurnEnded()
    {
        On = ++Ticks % Period == 0 ? true : false;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        var periodRemainder = Ticks % Period;
        if (periodRemainder == 0)
        {
            PlayOnAnimation();
        }   
        else
        {
            SpriteRenderer.sprite = periodRemainder == _ticksBeforeActivating ? ReadySprite : AlmostReadySprite;
        }
    }

    private void PlayOnAnimation()
    {
        if (_onAnimation != null)
        {
            _onAnimation.Kill();
        }

        _onAnimation = DOTween.Sequence();
        foreach (var onFrame in OnSprites)
        {
            _onAnimation.AppendCallback(() => SpriteRenderer.sprite = onFrame)
                .AppendInterval(0.050f);
        }
        _onAnimation.Play();
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        if (!On)
        {
            return;
        }

        foreach (var gameObject in steppedBy.GetComponents<Death>())
        {
            gameObject.Die();
        }
    }
}
