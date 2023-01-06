using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    public Label Target;

    private void OnTriggerStay2D(Collider2D other)
    {
        var label = other.GetComponent<Label>() ?? other.transform.parent.GetComponentInChildren<Label>();
        if (label == null)
        {
            return;
        }
        if (Target != null && Target.transform.DistanceTo(transform) < label.transform.DistanceTo(transform))
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
        }
    }
}
