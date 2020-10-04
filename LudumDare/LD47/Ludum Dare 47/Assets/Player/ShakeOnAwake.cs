using DG.Tweening;
using UnityEngine;

public class ShakeOnAwake : MonoBehaviour
{
    private Tween _shaking;

    private void OnEnable()
    {
        _shaking?.Kill();

        var targetTransform = transform.parent ?? transform;

        _shaking = DOTween.Sequence()
            .Append(targetTransform.DOLocalMoveX(0.02f, 0.05f))
            .Append(targetTransform.DOLocalMoveX(-0.02f, 0.05f))
            .SetLoops(-1, LoopType.Yoyo)
            .SetRelative(true)
            .Play();
    }

    private void OnDisable()
    {
        _shaking?.Kill();
    }
}
