using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TitleBehaviour : MonoBehaviour
{
    public UnityEvent OnReady;

    public float UpPositionY;
    public float UpDuration;

    public void GoUp()
    {
        Time.timeScale = 1;
        var rends = GameObject.Find("Border").GetComponentsInChildren<SpriteRenderer>();

        HeroStats.Reset();
        transform.DOLocalMoveY(UpPositionY, UpDuration);
        DOTween.Sequence()
            .AppendInterval(1)
            .AppendCallback(() =>
            {
                foreach (var rend in rends)
                {
                    DOTween.To(() => rend.color, (value) => rend.color = value, Color.white, 1);
                }
            })
            .AppendInterval(1)
            .AppendCallback(() =>
            {
                OnReady.Invoke();
            })
            .SetUpdate(true)
            .Play();
    }
}
