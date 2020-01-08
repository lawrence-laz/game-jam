using UnityEngine;
using UnityEngine.Events;

namespace Libs.Base.GameLogic
{
    public class SingleEventBehaviour : MonoBehaviour
    {
        public bool IsEnabled;
        public UnityEvent OnFirstUpdate;

        private void Update()
        {
            OnFirstUpdate.Invoke();
            Destroy(this);
        }
    }
}
