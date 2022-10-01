using DG.Tweening;
using UnityEngine;

namespace Libs.Base.Effects
{
    public class StartOnlyConfig : MonoBehaviour
    {
        void Start()
        {
            DOTween.SetTweensCapacity(400, 200);
        }
    }
}
