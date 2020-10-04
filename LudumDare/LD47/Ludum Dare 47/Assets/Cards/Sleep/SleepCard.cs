using DG.Tweening;
using UnityEngine;

public class SleepCard : MonoBehaviour
{
    public int RestoreEnergyBy = Stats.MaxEnergyPoints;

    public Card Card { get; set; }

    private void Start()
    {
        Card = GetComponent<Card>();
        Card.OnActivated.AddListener(OnActivated);
        Card.OnPlaced.AddListener(OnPlaced);
        Card.OnActivationFinished.AddListener(OnActivationFinished);
        Card.OnActivationStepTween.Add(OnActivationStep);
    }

    private Tween OnActivationStep()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();

        return DOTween.Sequence()
            .Append(DOTween.To(() => stats.Stress, x => stats.Stress = x, 0.2f / Card.Duration, 0.1f))
            .Append(DOTween.To(() => stats.Hunger, x => stats.Hunger = x, -Stats.HungerPerHour * 0.5f, 0.1f))
            .SetRelative(true);
    }

    private void OnActivated()
    {
        FindObjectOfType<Clock>().StepDuration = 0.3f;

        var player = GameObject.FindGameObjectWithTag("Player");
        var stats = FindObjectOfType<Stats>();

        var sleepAmount = Mathf.Clamp(Stats.MaxEnergyPoints - stats.Energy + 1, 0, Stats.MaxEnergyPoints);

        // Limiting sleep length to 10.
        sleepAmount = Mathf.Min(sleepAmount, 10);

        Card.EnergyCost = -sleepAmount; // Wake up on full energy.

        Card.BeforeActivationTween.Add(DOTween.Sequence()
            .Append(player.transform.DOLocalMoveY(-2, 1))
            .AppendCallback(() =>
            {
                var face = player.GetComponent<Face>();
                face.SetFace(face.Sleep);
            })
            .Append(player.transform.DOLocalMoveY(2, 1))
            .SetRelative(true)
        );
    }

    private void OnActivationFinished()
    {
        FindObjectOfType<Clock>().ResetStepDuration();
        FindObjectOfType<Deck>().Reshuffle();

        var player = GameObject.FindGameObjectWithTag("Player");
        DOTween.Sequence()
            .Append(player.transform.DORotate(Vector3.forward * -90, 1.5f).SetEase(Ease.InBack))
            .Join(player.transform.DOLocalMoveY(-2, 0.5f).SetDelay(1f))
            .AppendCallback(() =>
            {
                player.GetComponent<Face>().ResetFace();
                //player.transform.Translate(Vector3.down * 2);
                player.transform.rotation = Quaternion.identity;
            })
            .Append(player.transform.DOLocalMoveY(2, 1))
            .SetRelative(true)
            .Play();

        //.GetComponent<Face>().ResetFace();
    }

    private void OnPlaced(GameObject target)
    {
        Card.Activate();
    }
}
