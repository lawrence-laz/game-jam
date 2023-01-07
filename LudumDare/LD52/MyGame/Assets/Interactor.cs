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

        interaction.Invoke(this, InteractionArea.Target?.gameObject);

        return true;
    }
}
