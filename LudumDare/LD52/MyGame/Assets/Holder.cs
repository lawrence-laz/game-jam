using System.Collections.Generic;
using Libs.Base.Extensions;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public int MaxSlots = 5;
    public int CurrentSlots = 0;
    public Vector3 PositionOffsetBetweenItems = new(0f, 0.05f, -0.1f);
    public List<GameObject> Items = new();

    public bool TryPickUp(Pickable pickable)
    {
        if (CurrentSlots + pickable.Slots > MaxSlots)
        {
            TryDropAll();
        }

        Items.Add(pickable.gameObject);
        CurrentSlots += pickable.Slots;
        var pickableBody = pickable.GetComponent<Rigidbody2D>();
        if (pickableBody != null)
        {
            pickableBody.isKinematic = true;
        }
        pickable.gameObject.DisableAllComponentsInChildren<Collider2D>();
        pickable.transform.SetParent(transform);
        pickable.transform.localPosition = (Items.Count - 1) * PositionOffsetBetweenItems;
        pickable.transform.localRotation = Quaternion.identity;

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
            var pickableBody = item.GetComponent<Rigidbody2D>();
            if (pickableBody != null)
            {
                pickableBody.isKinematic = false;
            }
            item.gameObject.EnableAllComponentsInChildren<Collider2D>();
            item.transform.SetParent(null, worldPositionStays: true);
        }

        Items.Clear();

        return true;
    }
}
