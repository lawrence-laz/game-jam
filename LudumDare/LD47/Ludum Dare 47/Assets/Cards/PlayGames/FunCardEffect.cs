using DG.Tweening;
using UnityEngine;

public class FunCardEffect : MonoBehaviour
{
    public float RestoreFunBy = .75f;

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
            () => stats.Fun, 
            x => stats.Fun = x,
            RestoreFunBy / Card.EnergyCost, 
            clock.StepDuration)
            .SetRelative(true);
    }
}
