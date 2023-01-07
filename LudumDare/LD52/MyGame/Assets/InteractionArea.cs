using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    public Label Target;
    public bool TargetCanInteract;

    private void OnTriggerStay2D(Collider2D other)
    {
        var label = other.GetComponent<Label>() ?? other.transform.parent.GetComponentInChildren<Label>();
        if (label == null)
        {
            return;
        }

        var interaction = label.GetComponent<Interaction>();
        var interactor = GetComponentInParent<Interactor>();
        var canInteract = interaction != null && interaction.CanInvoke(interactor, label.gameObject);
        var currentInteractiveAndNewIsNot = TargetCanInteract && !canInteract;
        if (currentInteractiveAndNewIsNot)
        {
            return;
        }

        var currentIsCloser = Target != null && Target.transform.DistanceTo(transform) < label.transform.DistanceTo(transform);
        if (!TargetCanInteract && !canInteract && currentIsCloser)
        {
            return;
        }

        Target = label;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Target == null)
        {
            return;
        }

        if (other.transform?.parent == Target.transform || other.transform == Target.transform)
        {
            Target = null;
            TargetCanInteract = false;
        }
    }
}
