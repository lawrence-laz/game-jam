using UnityEngine;
using UnityEngine.UI;

public class FadeOutText : MonoBehaviour
{
    [SerializeField]
    float fadeStep;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        InvokeRepeating("FadeStep", 0, 1 / 23f);
    }

    private void FadeStep()
    {
        Color color = text.color;
        color.a = Mathf.Clamp01(color.a - fadeStep);
        text.color = color;
    }
}
