using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private string _importantNote;
    public string StandardNote;
    public const float ImportantNoteDuration = 5f;
 
    public string ImportantNote 
    {
        get => _importantNote;
        set
        {
            _importantNoteAddedAt = Time.unscaledTime;
            _importantNote = value;
        }
    }

    public Text Text { get; private set; }

    private float _importantNoteAddedAt;

    private void Start()
    {
        Text = GetComponent<Text>();    
    }

    private void Update()
    {
        if ((Time.unscaledTime - _importantNoteAddedAt) >= ImportantNoteDuration)
        {
            _importantNote = null;
        }

        Text.text = string.IsNullOrWhiteSpace(ImportantNote) ? StandardNote : ImportantNote;
    }
}
