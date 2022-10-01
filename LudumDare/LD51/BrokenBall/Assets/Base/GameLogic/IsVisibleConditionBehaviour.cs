using UnityEngine;

namespace Libs.Base.GameLogic
{
    public class IsVisibleConditionBehaviour : ConditionBehaviour
    {
        [SerializeField] Renderer _renderer = null;

        private void Update()
        {
            Value = _renderer.isVisible;
        }
    }
}
