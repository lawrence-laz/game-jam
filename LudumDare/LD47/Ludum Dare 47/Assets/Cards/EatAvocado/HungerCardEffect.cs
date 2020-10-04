using DG.Tweening;
using UnityEngine;

public class HungerCardEffect : MonoBehaviour
{
    public float RestoreHungerBy = 1f / (3 * 1.5f);

    public Card Card { get; set; }

    private void Start()
    {
        Card = GetComponent<Card>();
        Card.OnActivationStepTween.Add(OnActivationStep);
        Card.OnPlaced.AddListener(OnPlaced);
        Card.OnActivated.AddListener(OnActivated);
        Card.OnActivationFinished.AddListener(OnFinished);
    }

    private void OnFinished()
    {
        FindObjectOfType<Face>().ResetFace();
    }

    private void OnActivated()
    {
        var face = FindObjectOfType<Face>();
        face.SetFace(face.Eating);
    }

    private void OnPlaced(GameObject target)
    {
        Card.Activate();
    }

    private Tween OnActivationStep()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();

        return DOTween.To(
            () => stats.Hunger,
            x => stats.Hunger = x,
            RestoreHungerBy / Card.EnergyCost,
            0.1f)
            .SetRelative(true);
    }
}
