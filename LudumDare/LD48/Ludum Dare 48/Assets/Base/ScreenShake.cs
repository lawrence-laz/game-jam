using DG.Tweening;
using UnityEngine;
public class ScreenShake : MonoBehaviour
{
    public CustomImageEffect ImageEffect { get; set; }
    private Sequence _shake;
    private Vector3 _originalPosition;
    private int _strength;

    private void Start()
    {
        _originalPosition = transform.position;
        ImageEffect = FindObjectOfType<CustomImageEffect>();
    }

    public void VerySligthShake()
    {
        if (_shake != null)
        {
            return;
        }

        _strength = 1;
        _shake = DOTween.Sequence()
            .Append(transform.DOShakePosition(.5f, 0.01f, 40))
            .AppendCallback(() =>
            {
                transform.localPosition = _originalPosition;
                _shake = null;
                _strength = 0;
            });
    }

    public void SlightShake()
    {
        if (_shake != null)
        {
            if (_strength > 1) return;

            _shake.Kill();
            transform.localPosition = _originalPosition;
            _shake = null;
            ImageEffect.Split = 0;
            _strength = 0;
        }

        _strength = 1;

        _shake = DOTween.Sequence()
            .Append(transform.DOShakePosition(0.12f, 0.15f, 30))
            .AppendCallback(() =>
            {
                transform.localPosition = _originalPosition;
                _shake = null;
                _strength = 0;
            });
    }

    public void AverageShake()
    {
        if (_shake != null)
        {
            if (_strength > 2) return;

            _shake.Kill();
            transform.localPosition = _originalPosition;
            _shake = null;
            ImageEffect.Split = 0;
            _strength = 0;
        }

        _strength = 2;

        _shake = DOTween.Sequence()
            .Append(transform.DOShakePosition(0.12f, 0.2f, 50))
            .Join(DOTween.Shake(
                () => new Vector3(ImageEffect.Split, ImageEffect.Split, ImageEffect.Split),
                power => ImageEffect.Split = power.sqrMagnitude,
                0.12f, 10))
            .AppendCallback(() =>
            {
                transform.localPosition = _originalPosition;
                _shake = null;
                ImageEffect.Split = 0;
                _strength = 0;
            });
    }
}
