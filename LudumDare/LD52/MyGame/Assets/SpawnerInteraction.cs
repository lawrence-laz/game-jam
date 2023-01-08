using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class ChanceDrop
{
    public float Chance = 1;
    public GameObject Prefab;
}

public class SpawnerInteraction : Interaction
{
    public string InteractionText = "Open";
    public GameObject[] GuaranteedDropPrefabs;
    public ChanceDrop[] ChanceDropPrefabs;
    public string RequiredGameObjectLabel = "";
    public bool DestroyAfterInteraction;
    public bool DestroyRequiredGameObjectAfterInteraction;
    public GameObject DropToHand;
    public int InteractionsCount = 1;
    public Sprite SpriteAfterInteraction;
    public UnityEventGameObject OnInteraction;
    public TweenAnimation TweenAnimation;
    public Badges BadgeGive = Badges.None;

    public override string Text => InteractionText;

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        if (InteractionsCount < 1)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(RequiredGameObjectLabel))
        {
            var holder = interactor.GetComponentInChildren<Holder>();
            // Debug.Log($"Spawner, {RequiredGameObjectLabel}, items: {string.Join(", ", holder.Items.Select(x => x.gameObject.name))}. {holder.Items.Any(item => item.GetComponent<Label>().Is(RequiredGameObjectLabel))}");
            return holder.Items.Any(item => item.GetComponent<Label>().Is(RequiredGameObjectLabel));
        }

        return true;
    }

    public override Sequence Invoke(Interactor interactor, GameObject target)
    {
        if (TweenAnimation != null)
        {
            return TweenAnimation?.Run(() => Invocationlogic(interactor));
        }
        else
        {
            Invocationlogic(interactor);
            return null;
        }
    }

    private void Invocationlogic(Interactor interactor)
    {
        foreach (var dropPrefab in GuaranteedDropPrefabs)
        {
            var drop = Instantiate(dropPrefab);
            drop.transform.position = transform.position;
            if (GuaranteedDropPrefabs.Length > 1)
            {
                drop.transform.position += (Vector3)UnityEngine.Random.insideUnitCircle * 0.2f;
            }
        }

        foreach (var chanceDrop in ChanceDropPrefabs)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= chanceDrop.Chance)
            {
                var drop = Instantiate(chanceDrop.Prefab);
                drop.transform.position = transform.position;
                break;
            }
        }

        OnInteraction.Invoke(gameObject);

        if (SpriteAfterInteraction != null)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = SpriteAfterInteraction;
        }

        InteractionsCount--;

        if (DestroyAfterInteraction)
        {
            Destroy(gameObject);
        }
        if (!string.IsNullOrEmpty(RequiredGameObjectLabel) && DestroyRequiredGameObjectAfterInteraction)
        {
            var holder = interactor.GetComponentInChildren<Holder>();
            var item = holder.Items.FirstOrDefault(item => item.GetComponent<Label>().Is(RequiredGameObjectLabel));
            holder.TryDrop(item.GetComponent<Pickable>());
            Destroy(item.gameObject);
        }

        if (DropToHand != null)
        {
            var drop = Instantiate(DropToHand, transform.position, Quaternion.identity);
            drop.transform.rotation = Quaternion.identity;
            drop.GetComponent<PickupInteraction>().Invoke(interactor, drop);
        }

        Badge.Set(BadgeGive);
    }
}
