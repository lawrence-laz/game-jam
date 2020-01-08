using DG.Tweening;
using UnityEngine;

public class CrosshairStretch : MonoBehaviour
{
    private Sequence sequence;
    private bool _bufferedArrow;
    private float _downTime;
    private ArrowShoot _bow;
    private HeroStats _stats;
    private SpriteRenderer _sprite;

    private void OnEnable()
    {
        _bow = GetComponentInParent<ArrowShoot>();
        _stats = HeroStats.Get();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_bufferedArrow)
        {
            _downTime = Time.time;
            _sprite.color = new Color(1, 1, 1, 0);
            sequence = DOTween.Sequence()
                .Append(transform.DOScaleY(1, _stats.ArrowStretchTime).SetEase(Ease.OutQuart))
                .Join(_sprite.DOColor(Color.white, _stats.ArrowStretchTime).SetEase(Ease.OutQuart))
                .AppendCallback(()=>_sprite.DOColor(Color.cyan, 0.25f));
        }
        if (Input.GetMouseButtonUp(0) || _bufferedArrow)
        {
            if (Time.time - _downTime < _bow.MinArrowStretchTime)
            {
                _bufferedArrow = true;
                return;
            }

            _bufferedArrow = false;

            if (sequence != null)
            {
                sequence.Kill(true);
                transform.SetLocalScaleY(0);
            }
        }
    }
}
