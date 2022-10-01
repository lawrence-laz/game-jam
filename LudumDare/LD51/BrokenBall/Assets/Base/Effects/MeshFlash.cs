using UnityEngine;

namespace Libs.Base.Effects
{
    public class MeshFlash : MonoBehaviour
    {
        private Material _defaultShader;
        public Material _whiteShader;
        private MeshRenderer _renderer;

        private void OnEnable()
        {
            _renderer = GetComponent<MeshRenderer>();
            _defaultShader = _renderer.material;
        }

        public void WhiteSprite()
        {
            _renderer.material = _whiteShader;
            _renderer.material.color = Color.white;
        }

        public void NormalSprite()
        {
            _renderer.material = _defaultShader;
            _renderer.material.color = Color.white;
        }

        private void OnCollisionEnter(Collision collision)
        {
            WhiteSprite();
            Invoke("NormalSprite", 0.05f);
        }
    }
}
