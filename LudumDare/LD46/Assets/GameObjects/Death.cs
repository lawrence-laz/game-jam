using DG.Tweening;
using Libs.Base.Effects;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; set; }
    public TileObject TileObject { get; set; }
    public TurnManager TurnManager { get; set; }
    public ScreenShake ScreenShake { get; set; }

    public GameObject DeadPrefab;
    public UnityEvent OnDeath;

    private void Start()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        TileObject = GetComponent<TileObject>();
        TurnManager = FindObjectOfType<TurnManager>();
        ScreenShake = FindObjectOfType<ScreenShake>();
    }

    private void Update()
    {
        var a = "LUDUM DARE";
        // This sets enabled = true.
    }

    public void Die()
    {
        enabled = false;
        if (TryGetComponent<SpriteFlash>(out var flash))
        {
            flash.WhiteSprite();
        }

        var sequence = DOTween.Sequence()
            .AppendInterval(TurnManager.TurnDuration / 4);

        if (gameObject.tag != "Player" && GetComponent<Hostage>() == null)
        {
            sequence = sequence.AppendCallback(() =>
            {
                Thread.Sleep(50);
                ScreenShake.SlightShake();
            });
        }
        else
        {
            sequence = sequence.AppendCallback(() =>
            {
                TurnManager.enabled = false;
                ScreenShake.AverageShake();
            })
            .AppendInterval(TurnManager.TurnDuration * 1.5f);
        }
        sequence.AppendCallback(() =>
        {
            Destroy(gameObject);
            var obj = Instantiate(DeadPrefab, TileObject.Position, Quaternion.identity);
            if (obj.TryGetComponent<TileObject>(out var deadTile))
            {
                deadTile.Position = TileObject.Position;
            }
        })
        .Play();
    }
}
