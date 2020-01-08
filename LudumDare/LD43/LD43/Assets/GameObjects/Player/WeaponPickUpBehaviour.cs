using DG.Tweening;
using UnityEngine;
using UnityGoodies;

public class WeaponPickUpBehaviour : MonoBehaviour
{
    internal void PickUp(GameObject targetObject)
    {
        var weapon = targetObject.transform.GetLastParentWithSameLayer();
        weapon.SetParent(transform, true);

        var anim = DOTween.Sequence()
            .Append(weapon.DOLocalMove(Vector3.zero, 0.6f))
            .Join(weapon.DOLocalRotate(Vector3.zero, 0.6f))
            .Join(weapon.GetChild(0).DOLocalMove(Vector3.zero, 0.6f))
            .Join(weapon.GetChild(0).DOLocalRotate(Vector3.zero, 0.6f));
    }
}
