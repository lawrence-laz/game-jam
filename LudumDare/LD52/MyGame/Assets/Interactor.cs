using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public InteractionArea InteractionArea;
    public bool IsBusy => _currentSequence != null;

    private Sequence _currentSequence;

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
            var sequence = interaction.Invoke(
                this,
                InteractionArea?.Target?.gameObject);
            if (sequence != null)
            {
                _currentSequence = sequence;
            }
        }
        else
        {
            var sequence = interaction.Invoke(this, null);
            if (sequence != null)
            {
                _currentSequence = sequence;
            }
        }

        return true;
    }

    private void Update()
    {
        if (_currentSequence != null && (!_currentSequence.IsActive() || _currentSequence.IsComplete()))
        {
            _currentSequence = null;
        }
    }
}
