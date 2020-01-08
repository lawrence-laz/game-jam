using UnityEngine;
using UnityEngine.UI;

public class Speech : Singleton<Speech>
{
    Text bubbleText;
    Image bubble;

    public Sprite SkipSprite;
    public Sprite DefaultSprite;
    public Sprite CancelSprite;

    private void Start()
    {
        bubbleText = GetComponentInChildren<Text>();
        bubbleText.enabled = false;
        bubble = GetComponentInChildren<Image>();
        bubble.enabled = false;
    }

    public void Say(string text)
    {
        bubble.enabled = true;
        bubbleText.text = text;
        bubbleText.enabled = true;
    }

    public void Skippable()
    {
        bubble.sprite = SkipSprite;
    }

    public void NotSkippable()
    {
        bubble.sprite = DefaultSprite;
    }

    public void Cancelable()
    {
        bubble.sprite = CancelSprite;
    }

    public void Hide()
    {
        bubble.enabled = false;
        bubbleText.enabled = false;
    }
}
