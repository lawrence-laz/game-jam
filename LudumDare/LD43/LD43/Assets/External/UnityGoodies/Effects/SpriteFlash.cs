using DG.Tweening;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public Color Color = Color.white;

    private Shader _defaultShader;
    private Shader _whiteShader;
    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultShader = _renderer.material.shader;
        _whiteShader = Shader.Find("GUI/Text Shader");
    }

    public void FlashSprite()
    {
        _renderer.material.shader = _whiteShader;
        _renderer.color = Color;
    }

    public void NormalSprite()
    {
        _renderer.material.shader = _defaultShader;
        _renderer.color = Color.white;
    }

    public void FlashSprite(float duration)
    {
        FlashSprite();
        DOTween.Sequence()
            .AppendInterval(duration)
            .AppendCallback(() => NormalSprite())
            .Play();
    }

    public void FlashSprite(float period, int count)
    {
        var sequence = DOTween.Sequence();
        for (int i = 0; i < count; i++)
        {
            sequence
                .AppendCallback(() => FlashSprite())
                .AppendInterval(period)
                .AppendCallback(() => NormalSprite())
                .AppendInterval(period);
        }
        sequence.Play();
    }
}