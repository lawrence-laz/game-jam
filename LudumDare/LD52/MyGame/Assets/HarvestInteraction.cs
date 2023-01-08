using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HarvestInteraction : Interaction
{
    public override string Text => "Harvest";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        return interactor?.GetComponentInChildren<Scythe>() != null
            && target?.GetComponent<Harvestable>() != null;
    }

    public override Sequence InnerInvoke(Interactor interactor, GameObject target)
    {
        target.GetComponent<Harvestable>().Harvest();
        return null;
    }
}
