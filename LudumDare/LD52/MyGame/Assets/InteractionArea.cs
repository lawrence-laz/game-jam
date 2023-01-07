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
        var other = _colliders
            .OrderBy(collider => collider.transform.parent == null
                ? Vector2.Distance(collider.transform.position, transform.position)
                : Vector2.Distance(collider.transform.parent.position, transform.position))
            .FirstOrDefault();

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
