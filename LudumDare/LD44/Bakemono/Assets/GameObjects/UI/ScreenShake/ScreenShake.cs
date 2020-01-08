using DG.Tweening;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Sequence _anim;

    public static ScreenShake Get()
    {
        return GameObject.Find("ScreenShake_Object").GetComponent<ScreenShake>();
    }

    public void ShortShake()
    {
        KillExisting();

        _anim = DOTween.Sequence()
            .Append(Camera.main.transform.DOShakePosition(0.1f, 0.1f));
    }

    public void MediumShake()
    {
        KillExisting();

        _anim = DOTween.Sequence()
            .Append(Camera.main.transform.DOShakePosition(0.4f, 0.3f, 30, 90, false, true));
    }

    public void HardShake()
    {
        KillExisting();

        _anim = DOTween.Sequence()
            .Append(Camera.main.transform.DOShakePosition(1.5f, 0.4f, 40, 0, false, true));
    }

    private void KillExisting()
    {
        if (_anim != null)
            _anim.Kill(true);
    }

    private void OnDestroy()
    {
        KillExisting();
    }
}
