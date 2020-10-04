using UnityEngine;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour
{
    public Color LowColorForeground;
    public Color HighColorForeground;
    public Color LowColorBackground;
    public Color HighColorBackground;

    public Image Foreground;
    public Image Background;

    public Slider Slider { get; private set; }

    private void Start()
    {
        Slider = GetComponent<Slider>();
    }

    private void Update()
    {
        Foreground.color = Color.Lerp(LowColorForeground, HighColorForeground, Slider.value);
        Background.color = Color.Lerp(LowColorBackground, HighColorBackground, Slider.value);
    }
}
