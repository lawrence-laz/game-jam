using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryLogic : MonoBehaviour
{
    HeroStats _stats;
    int _startedWithHealth;
    bool _restarting;

    private void OnEnable()
    {
        _stats = HeroStats.Get();
        _startedWithHealth = _stats.Health;
    }

    public void DoRetry()
    {
        if (_restarting)
        {
            return;
        }

        _restarting = true;
        HeroStats._health = _startedWithHealth;

        ScreenFade.Instance.FadeOut(() => {
            DOTween.Clear(true);
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex);
        });
    }
}
