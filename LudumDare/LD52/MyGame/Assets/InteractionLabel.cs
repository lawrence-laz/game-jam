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
        if (InteractionArea.Target == null)
        {
            var holder = Interactor.GetComponentInChildren<Holder>();
            var interactiveItem = holder.Items
                .FirstOrDefault(item => item.GetInvokableInteraction(Interactor) != null);
            if (interactiveItem == null)
            {
                ShowText(string.Empty, Vector2.zero);
            }
            else
            {
                // Debug.Log($"Interactive item: {interactiveItem.gameObject.name}", interactiveItem);
                var interaction = interactiveItem.GetInvokableInteraction(Interactor);
                var labelText = GetLabelText(interaction, null);
                ShowText(labelText, Interactor.transform.position + (Vector3)PositionOffsetFromTarget);
            }
        }
        else
        {
            // Debug.Log($"InteractionArea.Target: {InteractionArea.Target.gameObject.name}", InteractionArea.Target);
            var interaction = InteractionArea.Target.gameObject.GetInvokableInteraction(Interactor, InteractionArea.Target.gameObject);
            var labelText = GetLabelText(interaction, InteractionArea.Target);
            var targetPosition = (Vector2)InteractionArea.Target.transform.position + PositionOffsetFromTarget;
            ShowText(labelText, targetPosition);
        }
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
            labelText = interaction.Text.Contains("{held}")
                ? interaction.Text.Replace("{held}", TextTerm.Get("{held}"))
                : $"{interaction.Text} {labelText}";
        }

        return labelText;
    }
}
