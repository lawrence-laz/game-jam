using DG.Tweening;
using UnityEngine;

namespace Libs.Base.Effects
{
    public class SpriteFlash : MonoBehaviour
    {
        private Shader _defaultShader;
        private Shader _whiteShader;
        private SpriteRenderer _renderer;

        private Sequence _animation;

        private void OnEnable()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
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

        public void Blink()
        {
            _animation?.Kill();

            _animation = DOTween.Sequence()
                .AppendCallback(WhiteSprite)
                .AppendInterval(0.1f)
                .AppendCallback(NormalSprite)
                .AppendInterval(0.1f)
                .SetLoops(4);
        }
    }
}
