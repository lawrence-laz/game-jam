using DG.Tweening;
using UnityEngine;

public class SlideInOut : MonoBehaviour
{
    public Ease EaseAnim = Ease.OutQuart;
    public float Duration;
    public Vector3 SlideFromPosition;
    public bool SlideInOnEnable;

    private Sequence _anim;
    private Vector3? _targetPosition;

    private void OnEnable()
    {
        if (SlideInOnEnable)
        {
            SlideIn();
        }
    }

    [ContextMenu("Slide In")]
    public void SlideIn()
    {
        if (_anim != null)
        {
            _anim.Kill(true);
        }

        gameObject.SetActive(true);
        if (!_targetPosition.HasValue)
        {
            _targetPosition = transform.localPosition;
        }
        transform.localPosition = SlideFromPosition;

        _anim = DOTween.Sequence()
            .Append(transform.DOLocalMove(_targetPosition.Value, Duration).SetEase(EaseAnim).SetUpdate(true))
            .SetUpdate(true);
    }

    [ContextMenu("Slide Out")]
    public void SlideOut()
    {
        if (_anim != null)
        {
            _anim.Kill(true);
        }

        _anim = DOTween.Sequence()
            .Append(transform.DOLocalMove(SlideFromPosition, Duration).SetEase(EaseAnim).SetUpdate(true))
            .AppendCallback(() => gameObject.SetActive(false))
            .SetUpdate(true);
    }
}
