using UnityEngine;

public class PickupInteraction : Interaction
{
    public override string Text => "Pick up";

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        return target?.GetComponent<Pickable>() != null;
    }

    public override void Invoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        var pickable = target.GetComponent<Pickable>();
        holder.TryPickUp(pickable);
    }
}
