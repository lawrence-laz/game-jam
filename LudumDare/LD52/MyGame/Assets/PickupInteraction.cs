using UnityEngine;

public class PickupInteraction : Interaction
{
    public int Size = 1;

    public override string Text => "Pick up";

    public override void Invoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        var pickable = target.GetComponent<Pickable>();
        holder.TryPickUp(pickable);
    }
}
