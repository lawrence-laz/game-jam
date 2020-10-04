using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    public Calendar Calendar { get; private set; }
    public Tween NextEvent;
    public Text GameOverText;

    [ContextMenu("Fall to sleep")]
    public void FallAsleep()
    {
        Card.Animation.OnComplete(() => FindObjectOfType<SleepCard>().Card.PlaceOn(default));
    }

    private void Start()
    {
        Stats = FindObjectOfType<Stats>();
        Time.timeScale = 1;
        Calendar = FindObjectOfType<Calendar>();
        Calendar.OnNewDay.AddListener(OnNewDay);
    }

    public static bool IsSomethingInProgress()
    {
        return Card.Animation != null && Card.Animation.IsActive() && !Card.Animation.IsComplete();
    }

    private void OnNewDay()
    {
        if (_debugSpeed)
        {
            return;
        }    

        if (Calendar.Day == 2)
        {
            Time.timeScale = 1.4f;
        }
        else
        {
            Time.timeScale = Mathf.Min(7, Calendar.Day);
        }
    }

    bool _debugSpeed = false;

    private void Update()
    {
        if (Stats.Energy <= 0 && Card.Active?.GetComponent<SleepCard>() == null)
        {
            FallAsleep();
        }

        if (!IsGameOver)
        {
            if (Stats.Hunger <= 0 || Stats.Stress <= 0 || Stats.Fun <= 0)
            {
                if (Stats.Hunger <= 0)
                {
                    GameOverText.text = "You starved to death.";
                }
                else if (Stats.Stress <= 0)
                {
                    GameOverText.text = "You stressed out too much.";
                }
                else if (Stats.Fun <= 0)
                {
                    GameOverText.text = "You died of boredom.";
                }

                IsGameOver = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            Time.timeScale += 0.5f;
            Debug.Log($"Changed time scale to: {Time.timeScale}");
            _debugSpeed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            Time.timeScale -= 0.5f;
            Debug.Log($"Changed time scale to: {Time.timeScale}");
        }

        if (NextEvent != null && !IsSomethingInProgress())
        {
            Card.Animation = NextEvent;
            NextEvent.Play();
            NextEvent = null;
        }
    }
}
