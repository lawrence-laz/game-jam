using DG.Tweening;
using UnityEngine;

public class TorchBehaviour : MonoBehaviour
{
    public Vector2 Intensity;
    public float Flicker;

    private Light _light;

    private void Start()
    {
        _light = GetComponentInChildren<Light>();
        InvokeRepeating("FlickerLight", 0, Flicker);
    }

    private void FlickerLight()
    {
        var target = Random.Range(Intensity.x, Intensity.y);
        DOTween.To(()=> _light.intensity,
            x=> _light.intensity=x,
            target,
            Flicker-0.001f);
    }
}
