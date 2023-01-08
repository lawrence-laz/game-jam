using DG.Tweening;
using UnityEngine;

public class PickupInteraction : Interaction
{
    public override string Text => "Pick up";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        return target?.GetComponent<Pickable>() != null && !IsHoldingTool(interactor);
    }

    public override Sequence Invoke(Interactor interactor, GameObject target)
    {
        if (IsHoldingTool(interactor))
        {
            return null;
        }

        var holder = interactor.GetComponentInChildren<Holder>();
        var pickable = target.GetComponent<Pickable>();
        holder.TryPickUp(pickable);
        return null;
    }

    private bool IsHoldingTool(Interactor interactor)
    {
        return interactor.GetComponentInChildren<ToolItem>() != null;
    }
}
