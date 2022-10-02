using System.Threading;
using DG.Tweening;
using UnityEngine;

namespace Libs.Base.Effects
{
    public class SpriteFlash : MonoBehaviour
    {
        public Color Color = Color.white;

        private Shader _defaultShader;
        private Shader _whiteShader;
        private SpriteRenderer _renderer;

        private Sequence _animation;
        private Color _originalColor;

        private void OnEnable()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _defaultShader = _renderer.material.shader;
            _whiteShader = Shader.Find("GUI/Text Shader");
        }

        public void WhiteSprite()
        {
            _originalColor = _renderer.color;
            _renderer.material.shader = _whiteShader;
            _renderer.color = Color;
        }

        public void NormalSprite()
        {
            _renderer.material.shader = _defaultShader;
            _renderer.color = _originalColor;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
            {
                Blink();
            }
        }

        public void Blink()
        {
            _animation?.Kill();

            _animation = DOTween.Sequence()
                .AppendCallback(WhiteSprite)
                .AppendInterval(0.05f)
                .AppendCallback(NormalSprite)
                .AppendInterval(0.05f)
                .SetLoops(4);
        }

        public void Blink(int loops, out Sequence animation)
        {
            _animation?.Kill();

            _animation = DOTween.Sequence()
                .AppendCallback(WhiteSprite)
                .AppendInterval(0.05f)
                .AppendCallback(() => Thread.Sleep(20))
                .AppendCallback(NormalSprite)
                .AppendInterval(0.05f)
                .SetLoops(loops);

            animation = _animation;
        }
    }
}
