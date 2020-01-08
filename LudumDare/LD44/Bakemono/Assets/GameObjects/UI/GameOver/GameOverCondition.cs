using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class GameOverCondition : MonoBehaviour
{
    public UnityEvent OnGameOver;
    public bool IsGameOver;

    HeroStats _stats;

    private void OnEnable()
    {
        Time.timeScale = 1;
        _stats = HeroStats.Get();
    }

    private void Update()
    {
        CheckAndInvokeGameOver();
    }

    private void CheckAndInvokeGameOver()
    {
        if (_stats.Health > 0 || IsGameOver)
        {
            return;
        }

        IsGameOver = true;
        OnGameOver.Invoke();
        DOTween.To(
            () => Time.timeScale,
            (value) => Time.timeScale = value,
            0,
            0.9f)
            .SetUpdate(true);

        FindObjectOfType<PlayerAim>().enabled = false;
    }
}
