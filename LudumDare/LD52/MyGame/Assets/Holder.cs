using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public int MaxSlots = 5;
    public int CurrentSlots = 0;
    public List<GameObject> Items = new();

    public bool TryPickUp(Pickable pickable)
    {
        if (CurrentSlots + pickable.Slots > MaxSlots)
        {
            return false;
        }

        Items.Add(pickable.gameObject);
        CurrentSlots += pickable.Slots;
        var follow = pickable.gameObject.AddComponent<DistanceFollow>();
        follow.Target = transform;

        return true;
    }

    public bool TryDropAll()
    {
        if (Items.Count == 0)
        {
            return false;
        }

        CurrentSlots = 0;

        foreach (var item in Items)
        {
            Destroy(item.GetComponent<DistanceFollow>());
        }

        Items.Clear();

        return true;
    }
}
