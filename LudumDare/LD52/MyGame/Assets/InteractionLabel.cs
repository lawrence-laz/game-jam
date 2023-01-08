using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

public class InteractionLabel : MonoBehaviour
{
    public InteractionArea InteractionArea;
    public Interactor Interactor;
    public Vector2 PositionOffsetFromTarget = Vector2.one;

    [Header("Visuals")]
    public GameObject VisualRootObject;
    public TextMeshProUGUI InteractionText;

    private void Update()
    {
        if (InteractionArea.Target == null || InteractionArea.Target.gameObject.GetInvokableInteraction(Interactor, InteractionArea.Target.gameObject) == null)
        {
            var holder = Interactor.GetComponentInChildren<Holder>();
            var interactiveItem = holder.Items
                .FirstOrDefault(item => item.GetInvokableInteraction(Interactor) != null);
            if (interactiveItem == null)
            {
                ShowText(string.Empty, Vector2.zero);
                if (InteractionArea.Target == null)
                {
                    return;
                }
            }
            else
            {
                // Debug.Log($"Interactive item: {interactiveItem.gameObject.name}", interactiveItem);
                var interaction = interactiveItem.GetInvokableInteraction(Interactor);
                var labelText = GetLabelText(interaction, null);
                ShowText(labelText, Interactor.transform.position + (Vector3)PositionOffsetFromTarget);
                return;
            }
        }
        // Debug.Log($"InteractionArea.Target: {InteractionArea.Target.gameObject.name}", InteractionArea.Target);
        var interactionn = InteractionArea.Target.gameObject.GetInvokableInteraction(Interactor, InteractionArea.Target.gameObject);
        var labelTextt = GetLabelText(interactionn, InteractionArea.Target);
        var targetPosition = (Vector2)InteractionArea.Target.transform.position
            + PositionOffsetFromTarget
            // + (Vector2)Interactor.transform.DirectionTo(InteractionArea.Target.transform) * PositionOffsetFromTarget.magnitude
            ;
        ShowText(labelTextt, targetPosition);
    }

    private void ShowText(string text, Vector2 position)
    {
        InteractionText.text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text);
        VisualRootObject.SetActive(text != string.Empty);
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    private string GetLabelText(Interaction interaction, Label target)
    {
        var labelText = target?.Text ?? string.Empty;
        if (interaction != null)
        {
            if (interaction.Text.Contains("{"))
            {
                labelText = interaction.Text
                    .Replace("{held}", TextTerm.Get("{held}"))
                    .Replace("{verb}", TextTerm.Get("{verb}"));
            }
            else
            {
                labelText = $"{interaction.Text} {labelText}";
            }
        }

        return labelText;
    }
}
