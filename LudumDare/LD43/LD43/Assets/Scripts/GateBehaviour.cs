using DG.Tweening;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    public Vector3 BreakingRotation;
    private bool _broken = false; 

    public void BreakOpen()
    {
        if (_broken)
            return;

        Debug.LogFormat("Entrance {0} was broken.", gameObject.name);

        _broken = true;
        transform.DOLocalRotate(BreakingRotation, 1).SetEase(Ease.OutElastic);
    }
}
