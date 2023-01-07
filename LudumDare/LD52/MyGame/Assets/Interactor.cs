using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public InteractionArea InteractionArea;

    public bool TryInteract()
    {
        var interactionGameObject = InteractionArea.Target?.gameObject;
        if (interactionGameObject == null)
        {
            var holder = GetComponentInChildren<Holder>();
            var interactiveItem = holder.Items
                .FirstOrDefault(item => item.GetInvokableInteraction(this) != null);
            if (interactiveItem == null)
            {
                return false;
            }
            else
            {
                interactionGameObject = interactiveItem;
            }
        }
        var interaction = interactionGameObject.GetInvokableInteraction(this, InteractionArea.Target?.gameObject);
        if (interaction == null)
        {
            return false;
        }

        if (InteractionArea.Target != null && InteractionArea.Target?.gameObject != null)
        {
            Debug.Log($"What is this? {gameObject} -> {InteractionArea?.Target?.gameObject}", InteractionArea?.Target?.gameObject);
            interaction.Invoke(
                this, 
                InteractionArea?.Target?.gameObject);
        }
        else
        {
            interaction.Invoke(this, null);
        }

        return true;
    }
}
