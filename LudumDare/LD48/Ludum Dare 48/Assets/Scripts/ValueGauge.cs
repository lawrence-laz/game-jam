using DG.Tweening;
using Shapes;
using UnityEngine;

public class ValueGauge : MonoBehaviour
{
    public Value value;
    public Color Full;
    public Color Empty;
    public Color Blink = Color.clear;

    public float BlinkFrom = -1;
    public float BlinkTo = -1;
    public float BlinkFrequency; // Times per second.

    private Disc _gauge;
    private Sequence _blinking;

    private void Start()
    {
        _gauge = GetComponent<Disc>();
    }

    private void Update()
    {
        _gauge.AngRadiansEnd = 2 * Mathf.PI * value.NormalizedValue;

        if (_blinking != null)
        {
            _gauge.Color = Color.Lerp(Empty, Full, value.NormalizedValue);
        }

        HandleBlinking();
    }

    private void HandleBlinking()
    {
        if (_blinking == null && value.NormalizedValue >= BlinkFrom && value.NormalizedValue <= BlinkTo)
        {
            Debug.Log($"Current {value.GetType().Name} value: {value.NormalizedValue}");

            _blinking = DOTween.Sequence()
                .Append(DOTween.To(() => _gauge.Color, x => _gauge.Color = x, Empty, 1f / BlinkFrequency / 2))
                .Append(DOTween.To(() => _gauge.Color, x => _gauge.Color = x, Blink, 1f / BlinkFrequency / 2))
                .SetLoops(-1, LoopType.Yoyo)
                .Play();
        }
        else if (_blinking != null && (value.NormalizedValue < BlinkFrom || value.NormalizedValue > BlinkTo))
        {
            _blinking.Kill();
            _blinking = null;
            _gauge.Color = Full;
        }
    }
}
