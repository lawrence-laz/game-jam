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
    }

    private void OnActivated()
    {
        FindObjectOfType<Clock>().StepDuration = 0.3f;
    }

    private void OnActivationFinished()
    {
        FindObjectOfType<Clock>().ResetStepDuration();
        FindObjectOfType<Deck>().Reshuffle();
    }

    private void OnPlaced(GameObject target)
    {
        if (target.GetComponent<PlayArea>() != null)
        {
            Card.Activate();
        }
    }
}
