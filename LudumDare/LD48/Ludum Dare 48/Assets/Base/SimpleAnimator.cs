using DG.Tweening;
using System;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    public Transform Target;
    public Ease _easeAnim = Ease.OutQuart;

    [Header("ScaleAndRotateRepeated")]
    public bool _scaleAndRotateRepeated;
    public float _scaleAndRotateRepeated_scaleMin = 0.6f;
    public float _scaleAndRotateRepeated_duration = .2f;
    public Vector3 _scaleAndRotateRepeated_rotate = Vector3.forward * 45f;

    [Header("UpAndDownRepeated")]
    public bool _upAndDownRepeated;
    public float _upOffset_upAndDownRepeated = .2f;
    public float _duration_upAndDownRepeated = .6f;

    [Header("ColorBlinkRepeated")]
    public bool _colorBlinkRepeated;
    public float _period_colorBlinkRepeated = 1;
    public Color _color_colorClinkRepeated = Color.white;

    private Sequence _animation;

    private void Start()
    {
        PlayDefault();
    }

    public void PlayDefault()
    {
        if (_scaleAndRotateRepeated)
        {
            ScaleAndRotateRepeated(_scaleAndRotateRepeated_scaleMin, _scaleAndRotateRepeated_duration, _scaleAndRotateRepeated_rotate);
        }
        else if (_upAndDownRepeated)
        {
            UpAndDownRepeated(_upOffset_upAndDownRepeated, _duration_upAndDownRepeated);
        }
        else if(_colorBlinkRepeated)
        {
            ColorBlinkRepeated(_period_colorBlinkRepeated, _color_colorClinkRepeated);
        }
    }

    public void ColorBlinkRepeated(float period, Color color)
    {
        if (Target == null)
            Target = transform;

        if (_animation != null)
            _animation.Kill(true);

        var spriteRenderer = Target.GetComponent<SpriteRenderer>();

        _animation = DOTween.Sequence()
            .Append(spriteRenderer.DOColor(color, period).SetEase(_easeAnim))
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
    }

    public void BlinkOnce(float period)
    {
        if (Target == null)
            Target = transform;

        if (_animation != null)
            _animation.Kill(true);

        var spriteRenderer = Target.GetComponent<SpriteRenderer>();
        var color = new Color(0, 0, 0, 0);

        _animation = DOTween.Sequence()
            .Append(spriteRenderer.DOColor(color, period).SetEase(_easeAnim))
            .Append(spriteRenderer.DOColor(Color.white, period).SetEase(_easeAnim))
            .AppendCallback(() =>
            {
                PlayDefault();
            });
    }

    public void UpAndDownOnce(float upOffset, float duration, Action<GameObject> midCallback = null, Action<GameObject> endCallback = null)
    {
        if (Target == null)
            Target = transform;

        if (_animation != null)
            _animation.Kill(true);

        _animation = DOTween.Sequence()
            .Append(Target.DOLocalMoveY(upOffset, duration).SetRelative(true))
            .AppendCallback(() =>
            {
                if (midCallback != null)
                {
                    midCallback.Invoke(Target.gameObject);
                }
            })
            .Append(Target.DOLocalMoveY(-upOffset, duration).SetRelative(true).SetEase(_easeAnim))
            .AppendCallback(() =>
            {
                if (endCallback != null)
                {
                    endCallback.Invoke(Target.gameObject);
                }

                PlayDefault();
            });
    }

    public void UpAndDownRepeated(float upOffset, float duration)
    {
        if (Target == null)
            Target = transform;

        if (_animation != null)
            _animation.Kill(true);

        _animation = DOTween.Sequence()
            .Append(Target.DOLocalMoveY(upOffset, duration).SetRelative(true).SetEase(_easeAnim))
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void ScaleAndRotateRepeated(float scaleMin, float duration, Vector3 rotate)
    {
        if (Target == null)
            Target = transform;

        if (_animation != null)
            _animation.Kill(true);

        _animation = DOTween.Sequence()
            .Append(Target.DOScale(scaleMin, duration))
            .Join(Target.DORotate(rotate, duration, RotateMode.LocalAxisAdd))
            .SetLoops(-1, LoopType.Yoyo);
    }
}
