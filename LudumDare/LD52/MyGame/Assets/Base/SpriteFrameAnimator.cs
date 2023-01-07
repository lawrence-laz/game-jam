using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpriteFrameAnimator : MonoBehaviour
{
    public Sprite[] Frames;
    public bool StartImmediately = true;
    public float FrameDuration = 0.2f;

    private Sequence _animation;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (StartImmediately)
        {
            StartAnimation();
        }
    }

    public void StartAnimation()
    {
        _animation = DOTween.Sequence()
            .AppendCallback(() => _spriteRenderer.sprite = Frames[0])
            .AppendInterval(FrameDuration)
            .AppendCallback(() => _spriteRenderer.sprite = Frames[1])
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopAnimation()
    {
        _animation?.Kill();
    }

    private void OnDestroy() {
        StopAnimation();
    }
}
