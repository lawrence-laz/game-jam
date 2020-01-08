using UnityEngine;

namespace Libs.Base.GameLogic
{
    public class IsVisibleConditionBehaviour : ConditionBehaviour
    {
        [SerializeField] Renderer _renderer;

        private void Update()
        {
            Value = _renderer.isVisible;
        }
    }
}
