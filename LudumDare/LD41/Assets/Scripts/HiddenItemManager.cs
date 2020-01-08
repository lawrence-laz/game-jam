using Assets.External.DreamBit.Extension;
using UnityEngine;

public class HiddenItemManager : MonoBehaviour
{
    public void HideItem(Collider collider)
    {
        collider.gameObject.AddComponentIfDoesntExist<HiddenItemBehaviour>();
    }

    public void UnhideItem(Collider collider)
    {
        collider.gameObject.RemoveAllComponents<HiddenItemBehaviour>();
    }
}
