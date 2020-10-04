using DG.Tweening;
using UnityEngine;

public class CoffeeCard : MonoBehaviour
{
    public float IncreasesStress = 1f / (4);
    public int IncreasesEnergy = Stats.MaxEnergyPoints / 4;

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
    }

    private void OnPlaced(GameObject target)
    {
        Card.Activate();
    }

    private Tween OnActivated()
    {
        var stats = FindObjectOfType<Stats>();
        var clock = FindObjectOfType<Clock>();

        var player = GameObject.FindGameObjectWithTag("Player");
        var face = player.GetComponent<Face>();
        face.SetFace(face.ShakyHappy);

        FindObjectOfType<SoundMaster>().Play(FindObjectOfType<SoundMaster>().Drink);

        return DOTween.Sequence()
            .Append(DOTween.To(() => stats.Stress, x => stats.Stress = x, -IncreasesStress, 0.1f))
            .Append(DOTween.To(() => stats.Energy, x => stats.Energy = x, IncreasesEnergy, 0.1f))
            .Join(DOTween.To(() => stats.Hunger, x => stats.Hunger = x, 0.05f, 0.1f))
            .SetRelative(true);
    }
}
