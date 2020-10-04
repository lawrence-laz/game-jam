using DG.Tweening;
using UnityEngine;

public class FallAndDestroy : MonoBehaviour
{
    private void Start()
    {
        DOTween.Sequence()
            .Append(transform.DOMoveY(-2, 5))
            .Join(DOTween.Sequence()
                .Append(transform.DOShakeRotation(1, 1, 1, 1, fadeOut: false))
                .SetLoops(5, LoopType.Yoyo))
            .AppendCallback(() => Destroy(gameObject))
            .SetLink(gameObject)
            .Play();
    }
}
