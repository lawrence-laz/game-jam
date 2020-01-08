using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private Shader _defaultShader;
    private Shader _whiteShader;
    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultShader = _renderer.material.shader;
        _whiteShader = Shader.Find("GUI/Text Shader");
    }

    public void WhiteSprite()
    {
        _renderer.material.shader = _whiteShader;
        _renderer.color = Color.white;
    }

    public void NormalSprite()
    {
        _renderer.material.shader = _defaultShader;
        _renderer.color = Color.white;
    }
}
