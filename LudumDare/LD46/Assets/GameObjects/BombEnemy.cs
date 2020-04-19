using DG.Tweening;
using Libs.Base.Effects;
using UnityEngine;
using UnityEngine.Events;

public class BombEnemy : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public Death Death { get; set; }
    public SpriteFlash SpriteFlash { get; set; }
    public TurnManager TurnManager { get; set; }

    public UnityEvent OnExplode;

    private Sequence _animation;

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        Death = GetComponent<Death>();
        SpriteFlash = GetComponent<SpriteFlash>();
        TurnManager = FindObjectOfType<TurnManager>();

        _animation = DOTween.Sequence()
            .Append(transform.DOShakePosition(0.2f, 0.05f, 1, 0))
            .SetLoops(-1);
    }

    private void OnDestroy()
    {
        if (_animation != null)
        {
            _animation.Kill();
        }
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        OnExplode.Invoke();
        SpriteFlash.Blink();
        var duration = 0.5f;
        TurnManager.AnimationDelay = duration + .1f;
        _animation?.Kill();
        _animation = DOTween.Sequence()
            .AppendInterval(duration)
            .AppendCallback(() => Death.Die())
            .Play();
    }
}
