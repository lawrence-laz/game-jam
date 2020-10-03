using DG.Tweening;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform[] Slots;

    public bool HasEmptySlot()
    {
        return Slots.Any(x => x.childCount == 0);
    }

    public void AddToHand(Transform card)
    {
        foreach (var slot in Slots)
        {
            if (slot.childCount == 0)
            {
                card.SetParent(slot, true);
                card.DOLocalMove(Vector3.zero, 0.5f);
                card.DOLocalRotateQuaternion(Quaternion.identity, 0.3f);
                
                return;
            }
        }

        Debug.LogWarning("Unable to draw card, to empty slots.");
    }

    public void RemoveFromHand(Transform transform)
    {
        foreach (var slot in Slots)
        {
            if (transform.parent == slot)
            {
                transform.parent = null;
                
                return;
            }
        }

        Debug.LogWarning($"Object '{transform.gameObject.name}' was not in hand, couldn't remove.");
    }

    public Tween HideHand()
    {
        return transform.DOMove(Vector3.down * 2, 0.7f).SetRelative(true);
    }

    public Tween ShowHand()
    {
        return transform.DOMove(new Vector3(0, -0.163f, -7.42f), 0.7f).SetRelative(false);
    }
}
