using DG.Tweening;
using UnityEngine;

public class FunCardEffect : MonoBehaviour
{
    public float RestoreFunBy = .75f;

    public Card Card { get; set; }

    private void Start()
    {
        Card = GetComponent<Card>();
        Card.OnActivated.AddListener(OnActivated);
        Card.OnActivationStepTween.Add(OnActivationStep);
        Card.OnActivationFinished.AddListener(OnActivationFinished);
        Card.OnPlaced.AddListener(OnPlaced);
    }

    private void OnActivated()
    {
        FindObjectOfType<Clock>().StepDuration = 1f;
    }

    private void OnActivationFinished()
    {
        FindObjectOfType<Clock>().ResetStepDuration();
    }

    private void OnPlaced(GameObject target)
    {
        //if (target.GetComponent<PlayArea>() != null)
        //{
        //}
            Card.Activate();
    }

    private Tween OnActivationStep()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();

        return DOTween.Sequence()
            .Append(DOTween.To(() => stats.Fun, x => stats.Fun = x, RestoreFunBy / Card.EnergyCost, 0.1f))
            .Append(DOTween.To(() => stats.Hunger, x => stats.Hunger = x, -Stats.HungerPerHour, 0.1f))
            .SetRelative(true);
    }
}
