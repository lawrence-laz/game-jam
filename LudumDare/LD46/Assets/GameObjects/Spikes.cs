using UnityEngine;

public class Spikes : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public TurnManager TurnManager { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    public Sprite OnSprite;
    public Sprite OffSprite;
    public Sprite ReadySprite;

    public bool On;
    public int Ticks;
    public const int Period = 3;
    private const int TicksBeforeActivating = Period - 1;

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        TurnManager = FindObjectOfType<TurnManager>();
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        SpriteRenderer = GetComponent<SpriteRenderer>();

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
        SpriteRenderer.sprite = periodRemainder == 0
            ? OnSprite
            : periodRemainder == TicksBeforeActivating
                ? ReadySprite
                : OffSprite;
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
