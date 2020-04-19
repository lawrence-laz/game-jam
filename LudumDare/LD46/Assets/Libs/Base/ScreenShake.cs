using DG.Tweening;
using UnityEngine;
public class ScreenShake : MonoBehaviour
{
    private Sequence _shake;
    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
    }

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
                transform.localPosition = _originalPosition;
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
                transform.localPosition = _originalPosition;
                _shake = null;
            });
    }

    public void AverageShake()
    {
        if (_shake != null)
        {
            _shake.Kill();
        }

        _shake = DOTween.Sequence()
            .Append(transform.DOShakePosition(0.12f, 0.2f, 50))
            .AppendCallback(() =>
            {
                transform.localPosition = _originalPosition;
                _shake = null;
            });
    }

}
