using System.Collections.Generic;
using Libs.Base.Extensions;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public int MaxSlots = 5;
    public int CurrentSlots = 0;
    public Vector3 PositionOffsetBetweenItems = new(0f, 0.05f, -0.1f);
    public List<GameObject> Items = new();
    public AudioClip DropSound;
    public AudioClip BucketDropSound;
    public AudioClip RigidDropSound;
    public AudioClip GenericDropSound;

    public bool TryPickUp(Pickable pickable)
    {
        var pickingUpBiggerItem = pickable.Slots > 1;
        if (CurrentSlots + pickable.Slots > MaxSlots)
        {
            if (pickingUpBiggerItem)
            {
                TryDropAll();
            }
            else
            {
                return false;
            }
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

    public bool TryDrop(Pickable pickable)
    {
        CurrentSlots -= pickable.Slots;
        Items.Remove(pickable.gameObject);
        Restore(pickable.gameObject);
        for (var i = 0; i < Items.Count; ++i)
        {
            var item = Items[i];
            item.transform.localPosition = i * PositionOffsetBetweenItems;
        }

        return true;
    }

    public bool TryDropAll()
    {
        if (Items.Count == 0)
        {
            return false;
        }

        CurrentSlots = 0;

        var soundPlayed = false;

        foreach (var item in Items)
        {
            Restore(item);

            if (item.GetComponent<ToolItem>())
            {
                soundPlayed = true;
                GetComponent<AudioSource>().PlayOneShot(DropSound);
            }
            else if (item.GetComponent<Bucket>())
            {
                soundPlayed = true;
                GetComponent<AudioSource>().PlayOneShot(BucketDropSound);
            }
            else if (item.GetComponent<Label>().Is("glass"))
            {
                soundPlayed = true;
                GetComponent<AudioSource>().PlayOneShot(RigidDropSound);
            }
        }

        if (!soundPlayed)
        {
            GetComponent<AudioSource>().PlayOneShot(GenericDropSound);
        }

        Items.Clear();

        return true;
    }

    private void Restore(GameObject item)
    {
        var pickableBody = item.GetComponent<Rigidbody2D>();
        if (pickableBody != null)
        {
            pickableBody.isKinematic = false;
        }
        item.gameObject.EnableAllComponentsInChildren<Collider2D>();
        item.transform.SetParent(null, worldPositionStays: true);
    }
}
