using UnityEngine;

public class PickupInteraction : Interaction
{
    public override string Text => "Pick up";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        return target?.GetComponent<Pickable>() != null && !IsHoldingScythe(interactor);
    }

    public override void Invoke(Interactor interactor, GameObject target)
    {
        if (IsHoldingScythe(interactor))
        {
            return;
        }

        var holder = interactor.GetComponentInChildren<Holder>();
        var pickable = target.GetComponent<Pickable>();
        holder.TryPickUp(pickable);
    }

    private bool IsHoldingScythe(Interactor interactor)
    {
        return interactor.GetComponentInChildren<Scythe>() != null;
    }
}
