using Libs.Base.GameLogic;
using System.Linq;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public TurnManager TurnManager { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public GameOver GameOver { get; set; }

    public Sprite OnSprite;
    public Sprite OffSprite;

    public bool On;

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        TurnManager = FindObjectOfType<TurnManager>();
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        GameOver = FindObjectOfType<GameOver>();
    }

    private void OnTurnEnded()
    {
        On = !On;
        SpriteRenderer.sprite = On ? OnSprite : OffSprite;
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        if (!On)
        {
            return;
        }

        if (steppedBy.Any(x => x.tag == "Player" || x.GetComponent<Hostage>() != null))
        {
            GameOver.Invoke();
        }
    }
}
