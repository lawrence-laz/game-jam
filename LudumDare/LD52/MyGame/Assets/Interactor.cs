using UnityEngine;

public class Interactor : MonoBehaviour
{
    public InteractionArea InteractionArea;

    public bool TryInteract()
    {
        if (InteractionArea.Target == null)
        {
            return false;
        }
        var interaction = InteractionArea.Target.GetComponent<Interaction>();
        if (interaction == null)
        {
            return false;
        }

        interaction.Invoke(this, InteractionArea.Target.gameObject);

        return true;
    }
}
