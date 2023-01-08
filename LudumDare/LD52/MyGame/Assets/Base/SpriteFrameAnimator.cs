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
    private Sprite _idleSprite;
    private int _frameIndex;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _idleSprite = _spriteRenderer.sprite;
        if (StartImmediately)
        {
            StartAnimation();
        }
    }

    public void NextFrame()
    {
        _spriteRenderer.sprite = Frames[_frameIndex];
        _frameIndex = (_frameIndex + 1) % Frames.Length;
    }

    public void StartAnimation()
    {
        if (_animation != null)
        {
            return;
        }

        _animation = DOTween.Sequence()
            .AppendCallback(() => _spriteRenderer.sprite = Frames[0])
            .AppendInterval(FrameDuration)
            .AppendCallback(() => _spriteRenderer.sprite = Frames[1])
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopAnimation()
    {
        _spriteRenderer.sprite = _idleSprite;

        if (_animation == null)
        {
            return;
        }

        _animation?.Kill();
        _animation = null;
    }

    private void OnDestroy() {
        StopAnimation();
    }
}
