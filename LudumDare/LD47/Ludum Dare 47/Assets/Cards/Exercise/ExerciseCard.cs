using DG.Tweening;
using UnityEngine;

public class ExerciseCard : MonoBehaviour
{
    public float RestoreStress = 1f / (3 * 2);

    public Card Card { get; set; }

    private void Start()
    {
        Card = GetComponent<Card>();
        Card.OnActivationStepTween.Add(OnActivated);
        Card.OnPlaced.AddListener(OnPlaced);
    }

    private void OnPlaced(GameObject target)
    {
        //if (target.GetComponent<PlayArea>() != null)
        //{
        //}
            Card.Activate();
    }

    private Tween OnActivated()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();

        return DOTween.Sequence()
            .Append(DOTween.To(() => stats.Stress, x => stats.Stress = x, RestoreStress / Card.EnergyCost, 0.1f))
            .Append(DOTween.To(() => stats.Hunger, x => stats.Hunger = x, -Stats.HungerPerHour * 2, 0.1f)) // Exercise makes you hungry faster.
            .SetRelative(true);
    }
}
