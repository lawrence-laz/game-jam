using DG.Tweening;
using UnityEngine;

public class FunCardEffect : MonoBehaviour
{
    public const float RestoreFunBy = .5f;

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
        FindObjectOfType<SoundMaster>().Play(FindObjectOfType<SoundMaster>().Gaming);
    }

    private void OnActivationFinished()
    {
        FindObjectOfType<Clock>().ResetStepDuration();
    }

    private void OnPlaced(GameObject target)
    {
        Card.Activate();
    }

    private Tween OnActivationStep()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();

        return DOTween.Sequence()
            .Append(DOTween.To(() => stats.Fun, x => stats.Fun = x, RestoreFunBy / Card.Duration, 0.1f))
            .Append(DOTween.To(() => stats.Stress, x => stats.Stress = x, clock.IsNight ? 0.1f : 0.2f / Card.Duration, 0.1f))
            .Append(DOTween.To(() => stats.Hunger, x => stats.Hunger = x, -Stats.HungerPerHour, 0.1f))
            .SetRelative(true);
    }
}
