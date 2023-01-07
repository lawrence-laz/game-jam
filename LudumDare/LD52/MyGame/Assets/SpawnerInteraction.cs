using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int InteractionsCount = 1;
    public Sprite SpriteAfterInteraction;
    public UnityEventGameObject OnInteraction;

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
            return holder.Items.Any(item => item.GetComponent<Label>().Text == RequiredGameObjectLabel);
        }

        return true;
    }

    public override void Invoke(Interactor interactor, GameObject target)
    {
        foreach (var dropPrefab in GuaranteedDropPrefabs)
        {
            var drop = Instantiate(dropPrefab);
            drop.transform.position = transform.position;
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
    }
}
