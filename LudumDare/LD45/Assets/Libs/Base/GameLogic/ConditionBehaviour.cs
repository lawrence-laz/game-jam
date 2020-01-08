using UnityEngine;

namespace Libs.Base.GameLogic
{
    public class ConditionBehaviour : MonoBehaviour
    {
        [SerializeField] bool _value;
        public bool Value { get { return _value; } set { _value = value; } }
    }
}
