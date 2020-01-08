using DG.Tweening;
using UnityEngine;

public class SlideInOut : MonoBehaviour
{
    public Ease EaseAnim = Ease.OutQuart;
    public float Duration;
    public float SlidedInX;
    public bool SlideInOnEnable;

    private Sequence _anim;

    private void OnEnable()
    {
        if (SlideInOnEnable)
        {
            SlideIn();
        }
    }

    public void SlideIn()
    {
        if (_anim != null)
        {
            _anim.Kill(true);
        }

        gameObject.SetActive(true);
        var pos = transform.localPosition;
        pos.x = SlidedInX - 20;
        transform.localPosition = pos;

        _anim = DOTween.Sequence()
            .Append(transform.DOLocalMoveX(SlidedInX, Duration).SetEase(EaseAnim).SetUpdate(true))
            .SetUpdate(true);
    }

    public void SlideOut()
    {
        if (_anim != null)
        {
            _anim.Kill(true);
        }

        _anim = DOTween.Sequence()
            .Append(transform.DOLocalMoveX(SlidedInX + 20, Duration).SetEase(EaseAnim).SetUpdate(true))
            .AppendCallback(() => gameObject.SetActive(false))
            .SetUpdate(true);
    }
}
