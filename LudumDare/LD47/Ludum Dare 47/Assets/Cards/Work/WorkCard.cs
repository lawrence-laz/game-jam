using DG.Tweening;
using UnityEngine;

public class WorkCard : MonoBehaviour
{
    public float IncreasesStress = 1f / 3.5f;

    public Card Card { get; set; }

    private void Start()
    {
        Card = GetComponent<Card>();
        Card.OnActivationStepTween.Add(OnActivated);
        Card.OnPlaced.AddListener(OnPlaced);
        Card.OnActivationFinished.AddListener(OnFinished);
    }

    private void OnFinished()
    {
        GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Face>()
            .ResetFace();

        FindObjectOfType<Clock>().ResetStepDuration();
    }

    private void OnPlaced(GameObject target)
    {
        Card.Activate();
    }

    private Tween OnActivated()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();
        
        FindObjectOfType<Clock>().StepDuration = 1f / 3;

        var player = GameObject.FindGameObjectWithTag("Player");
        var face = player.GetComponent<Face>();
        face.SetFace(face.Focused);

        return DOTween.Sequence()
            .Append(DOTween.To(() => stats.Fun, x => stats.Fun = x, -0.3333333f / Card.Duration, 0.1f))
            .Append(DOTween.To(() => stats.Stress, x => stats.Stress = x, -IncreasesStress / Card.Duration, 0.1f))
            .Append(DOTween.To(() => stats.Hunger, x => stats.Hunger = x, -Stats.HungerPerHour, 0.1f))
            .SetRelative(true);
    }
}
