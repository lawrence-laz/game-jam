using DG.Tweening;
using UnityEngine;

public class ShieldSpriteBehaviour : MonoBehaviour
{
    private HeroStats _stats;

    private void OnEnable()
    {
        _stats = HeroStats.Get();
    }

    private void Start()
    {
        if (_stats.HasShield)
        {
            _stats.Shield = 2;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnLost()
    {
        GetComponentInChildren<PlayRandomAudioCLip>().Play();
        DOTween.Sequence()
            .AppendInterval(0.3f)
            .AppendCallback(() => gameObject.SetActive(false));
    }
}
