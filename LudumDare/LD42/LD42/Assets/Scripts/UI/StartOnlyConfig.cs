using DG.Tweening;
using UnityEngine;

public class StartOnlyConfig : MonoBehaviour
{
    void Start()
    {
        DOTween.SetTweensCapacity(400, 200);
    }
}
