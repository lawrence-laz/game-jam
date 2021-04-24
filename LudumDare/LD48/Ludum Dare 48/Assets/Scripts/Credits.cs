using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public int Value = 1000;

    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = Value
            .ToString("n", CultureInfo.InvariantCulture)
            .Replace(',', ' ')
            .Split('.')[0];
    }
}
