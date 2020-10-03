using DG.Tweening;
using UnityEngine;

public class HungerCardEffect : MonoBehaviour
{
    public float RestoreHungerBy = 1f / (3 * 2);

    public Card Card { get; set; }

    private void Start()
    {
        Card = GetComponent<Card>();
        Card.OnActivationStepTween.Add(OnActivated);
        Card.OnPlaced.AddListener(OnPlaced);
    }

    private void OnPlaced(GameObject target)
    {
        if (target.GetComponent<PlayArea>() != null)
        {
            Card.Activate();
        }
    }

    private Tween OnActivated()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();

        return DOTween.To(
            () => stats.Hunger,
            x => stats.Hunger = x,
            RestoreHungerBy / Card.EnergyCost,
            clock.StepDuration)
            .SetRelative(true);
    }
}
