using DG.Tweening;
using UnityEngine;

public class HeroEffects : MonoBehaviour
{
    Sequence _sequence;
    Transform _spriteTransform;
    SpriteRenderer _sprite;

    private void OnEnable()
    {
        _spriteTransform = transform.Find("Hero_Sprite");
        _sprite = _spriteTransform.GetComponent<SpriteRenderer>();
    }

    public void OnDamaged()
    {
        foreach (var flash in GetComponentsInChildren<SpriteFlash>())
        {
            flash.WhiteSprite();
            DOTween.Sequence()
                .AppendInterval(.05f)
                .AppendCallback(() => flash.NormalSprite())
                .AppendInterval(0.05f)
                .AppendCallback(() => flash.WhiteSprite())
                .AppendInterval(.05f)
                .AppendCallback(() => flash.NormalSprite());
        }

        if (_sequence != null)
        {
            _sequence.Kill(true);
        }

        _sequence = DOTween.Sequence()
            .Append(_spriteTransform.DOScaleY(.7f, 0.1f).SetEase(Ease.OutQuad))
            .Join(_sprite.DOColor(Color.red, 0.1f))
            .AppendInterval(0.1f)
            .Append(_spriteTransform.DOScaleY(1, 0.2f).SetEase(Ease.OutQuad))
            .Join(_sprite.DOColor(Color.white, 0.2f));
    }
}
