using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    public UnityEvent OnGameOver;
    public bool IsGameOver;

    private void Update()
    {
        var balls = FindObjectsOfType<Ball>();
        if (!IsGameOver && balls.Length == 0)
        {
            Invoke();
        }
    }

    public void Invoke()
    {
        IsGameOver = true;
        OnGameOver.Invoke();
        // RestartLevel();
        FindObjectOfType<Timer>().StopTimer();
    }

    public void RestartLevel()
    {
        for (var i = 1; i < 100; ++i)
        {
            var level = GameObject.Find($"Level{i}");
            if (level != null && level.activeSelf)
            {
                level.GetComponent<ILevel>().Restart();
                return;
            }
        }
    }
}