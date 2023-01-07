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
            InteractionText.text = string.Empty;
            VisualRootObject.SetActive(false);
        }
        else
        {
            var labelText = InteractionArea.Target.Text;
            var interaction = InteractionArea.Target.GetComponent<Interaction>();
            if (interaction != null && interaction.CanInvoke(Interactor, InteractionArea?.Target.gameObject))
            {
                labelText = $"{interaction.Text} {labelText}";
            }
            InteractionText.text = labelText;
            VisualRootObject.SetActive(true);

            var targetPosition = (Vector2)InteractionArea.Target.transform.position + PositionOffsetFromTarget;
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }
    }
}
