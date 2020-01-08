using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Sequence _shake;


    public void VerySligthShake()
    {
        if (_shake != null)
        {
            return;
        }

        _shake = DOTween.Sequence()
            .Append(transform.DOShakePosition(.5f, 0.01f, 40))
            .AppendCallback(() =>
            {
                transform.localPosition = Vector3.zero;
                _shake = null;
            });
    }

    public void SlightShake()
    {
        if (_shake != null)
        {
            _shake.Kill();
        }

        _shake = DOTween.Sequence()
            .Append(transform.DOShakePosition(0.12f, 0.15f, 30))
            .AppendCallback(() =>
            {
                transform.localPosition = Vector3.zero;
                _shake = null;
            });
    }

    public void ScreenShake()
    {
        if (_shake != null)
        {
            _shake.Kill();
        }

        _shake = DOTween.Sequence()
            .Append(transform.DOShakePosition(0.12f, 0.2f, 50))
            .AppendCallback(() =>
            {
                transform.localPosition = Vector3.zero;
                _shake = null;
            });
    }
}
