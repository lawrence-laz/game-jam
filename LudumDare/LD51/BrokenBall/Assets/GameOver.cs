using System.Threading;
using DG.Tweening;
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
            IsGameOver = true;
            Invoke(nameof(DelayedGameOver), 0.7f);
        }
    }

    public void DelayedGameOver()
    {
        var balls = FindObjectsOfType<Ball>();
        if (balls.Length == 0)
        {
            Invoke();
        }
        else
        {
            IsGameOver = false;
        }
    }

    public void Invoke()
    {
        var upgrades = FindObjectsOfType<Upgrade>();
        foreach (var upgrade in upgrades)
        {
            Destroy(upgrade.gameObject);
        }
        IsGameOver = true;
        OnGameOver.Invoke();
        // RestartLevel();
        FindObjectOfType<Timer>().StopTimer();
    }

    public void InvokeCollidedWithObstacle(Vector2 position)
    {
        IsGameOver = true;
        var upgrades = FindObjectsOfType<Upgrade>();
        foreach (var upgrade in upgrades)
        {
            Destroy(upgrade.gameObject);
        }
        FindObjectOfType<Timer>().StopTimer();
        FindObjectOfType<Paddle>().enabled = false;
        // FindObjectOfType<ScreenShake>().MediumShake(); // Camera is busy showing where game over occured. Would need a separate transform.
        FindObjectOfType<GlobalAudio>().Play(GlobalAudio.Instance.LostBall);
        foreach (var rotator in FindObjectsOfType<Rotate>())
        {
            rotator.enabled = false;
        }

        DOTween.Sequence()
            .Append(Camera.main.transform.DOMove(new Vector3(position.x, position.y, Camera.main.transform.position.z), 0.5f)).SetEase(Ease.OutBounce)
            .Join(Camera.main.DOOrthoSize(-1, 1f).SetRelative(true))
            .AppendInterval(1)
            .AppendCallback(() => OnGameOver.Invoke())
            .Play();
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
