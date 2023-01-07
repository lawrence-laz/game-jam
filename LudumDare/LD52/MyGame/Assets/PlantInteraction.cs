using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Libs.Base.Extensions;
using UnityEngine;

public class PlantInteraction : Interaction
{
    public override string Text => "Plant {held}";
    public GameObject PlantedSeedPrefab;

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        return TryGetSeed(interactor, out var _);
    }

    public override void Invoke(Interactor interactor, GameObject target)
    {
        TryGetSeed(interactor, out var item);

        var result = Instantiate(PlantedSeedPrefab);
        result.transform.position = transform.position;
        result.EnableAllComponentsInChildren<Collider2D>();

        var holder = interactor.GetComponentInChildren<Holder>();
        holder.TryDrop(item.GetComponent<Pickable>());
        Destroy(item);

        Destroy(gameObject);
    }

    private bool TryGetSeed(Interactor interactor, out GameObject seed)
    {
        seed = null;
        var holder = interactor.GetComponentInChildren<Holder>();
        if (holder == null)
        {
            return false;
        }

        seed = holder.Items.FirstOrDefault(item => item.GetComponent<Label>().Text == "seed");
        if (seed != null)
        {
            TextTerm.Set("{held}", "seed");
        }

        return seed != null;
    }
}
