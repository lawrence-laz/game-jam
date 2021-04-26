using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public int Value = 1000;
    public static int ValueBetweenScenes = 0;

    private Text _text;

    private void Start()
    {
        Value = ValueBetweenScenes;
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        ValueBetweenScenes = Value;
        _text.text = Value
            .ToString("n", CultureInfo.InvariantCulture)
            .Replace(',', ' ')
            .Split('.')[0];
    }
}
