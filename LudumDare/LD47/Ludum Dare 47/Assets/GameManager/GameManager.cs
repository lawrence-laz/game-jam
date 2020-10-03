using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Stats Stats { get; private set; }

    [ContextMenu("Fall to sleep")]
    public void FallAsleep()
    {
        FindObjectOfType<Stats>().Energy = Stats.MaxEnergyPoints;
        FindObjectOfType<Deck>().Reshuffle();
    }

    private void Start()
    {
        Stats = FindObjectOfType<Stats>();
    }

    private void Update()
    {
        if (Stats.Energy <= 0)
        {
            FallAsleep();
        }
    }
}
