using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public string ImportantNote;
    public string StandardNote;

    public Text Text { get; private set; }

    private void Start()
    {
        Text = GetComponent<Text>();    
    }

    private void Update()
    {
        Text.text = string.IsNullOrWhiteSpace(ImportantNote) ? StandardNote : ImportantNote;
    }
}
