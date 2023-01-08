using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    public Label Target;
    public bool TargetCanInteract;

    public List<Collider2D> _colliders = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "TriggerArea")
        {
            // Don't trigger on other creatures trigger zone
            return;
        }
        _colliders.Add(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        RefocusTarget();
    }

    private void Update()
    {
        RefocusTarget();
    }

    private void RefocusTarget()
    {
        var interactor = GetComponentInParent<Interactor>();
        var ordered = _colliders
            .Select(x => x.transform.parent ?? x.transform)
            .OrderBy(collider =>
            {
                var canInteract = collider
                    .GetComponentsInChildren<Interaction>()
                    .Any(interaction => interaction != null && interaction.CanInvoke(interactor, collider.gameObject));
                return canInteract ? 0 : 1;
            })
            .ThenBy(collider => collider.transform.parent == null
                ? Vector2.Distance(collider.transform.position, transform.position)
                : Vector2.Distance(collider.transform.parent.position, transform.position))
            .ToList();

        // Debug.Log(string.Join(", ", ordered.Select(x => x.gameObject.name)));
        var other = ordered.FirstOrDefault();

        if (other == null)
        {
            SetTarget(null);
            TargetCanInteract = false;
            return;
        }

        var label = other.GetComponent<Label>() ?? other.transform.parent?.GetComponentInChildren<Label>();
        if (label == null)
        {
            return;
        }

        var canInteract = label
                    .GetComponentsInChildren<Interaction>()
                    .Any(interaction => interaction != null && interaction.CanInvoke(interactor, label.gameObject));
        // var interaction = label.GetComponent<Interaction>();
        // var canInteract = interaction != null && interaction.CanInvoke(interactor, label.gameObject);
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

        SetTarget(label);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _colliders.Remove(other);

        if (Target == null)
        {
            return;
        }

        if (other.transform?.parent == Target.transform || other.transform == Target.transform)
        {
            SetTarget(null);
            TargetCanInteract = false;
        }
    }

    private const float SCALE_ON_FOCUS = 1.2f;
    private void SetTarget(Label target)
    {
        if (Target != null)
        {
            var oldSprite = Target.GetComponentInChildren<SpriteRenderer>();
            oldSprite.color = Color.white;
            oldSprite.transform.localScale *= 1 / SCALE_ON_FOCUS;
        }
        Target = target;

        if (Target != null)
        {
            var currentSprite = Target.GetComponentInChildren<SpriteRenderer>();
            // currentSprite.color = Color.blue;
            currentSprite.transform.localScale *= SCALE_ON_FOCUS;
        }
    }
}
