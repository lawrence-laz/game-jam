using Libs.Base.GameLogic;
using System.Linq;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public TurnManager TurnManager { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

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
        UpdateSprite();
    }

    private void OnTurnEnded()
    {
        On = !On;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        SpriteRenderer.sprite = On ? OnSprite : OffSprite;
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
