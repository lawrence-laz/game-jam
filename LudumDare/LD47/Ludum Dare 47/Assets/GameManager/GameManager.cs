using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;

    public bool IsGameOver 
    {
        get => _isGameOver;
        set
        {
            if (value == _isGameOver)
            {
                return;
            }

            _isGameOver = value;

            if (_isGameOver)
            {
                OnGameOver.Invoke();
            }
        }
    }

    public UnityEvent OnGameOver;

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

        if (!IsGameOver)
        {
            if (Stats.Hunger <= 0 || Stats.Stress <= 0 || Stats.Fun <= 0)
            {
                IsGameOver = true;
            }
        }
    }
}
