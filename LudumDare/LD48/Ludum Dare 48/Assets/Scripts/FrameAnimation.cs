using UnityEngine;

public class FrameAnimation : MonoBehaviour
{
    public Sprite[] Frames;
    public float FrameDuration; // In seconds.

    private SpriteRenderer _spriteRenderer;
    private float _nextFrameAt;
    private int _currentFrameIndex;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangeFrame();
    }

    private void ChangeFrame()
    {
        if (Time.time < _nextFrameAt)
        {
            return;
        }

        _spriteRenderer.sprite = Frames[_currentFrameIndex++];
        _currentFrameIndex %= Frames.Length;

        _nextFrameAt = Time.time + FrameDuration;
    }
}
