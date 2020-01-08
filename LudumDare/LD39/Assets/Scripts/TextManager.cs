using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextManager : Singleton<TextManager>
{
    Text text;
    public string Text
    {
        get { return text.text; }
        set { text.text = value; }
    }

    Image background;

    string buffer;
    int bufferIndex = 0;

    string[] messages = new string[]{};
    int messagesIndex = 0;
    float typeDelay = 0.1f;
    bool allowMovement = true;
    public bool AllowMovement { get { return allowMovement && !isCurrentlyTyping; } }
    GameObject sender = null;

    bool isCurrentlyTyping = false;

    private void Start()
    {
        text = GetComponent<Text>();
        background = transform.parent.GetComponent<Image>();
    }

    private void Update()
    {
        if ((Action.Instance.action || messagesIndex < 0) && messages.Length > 0)
        {
            Action.Instance.action = false;

            if (!isCurrentlyTyping)
            {
                HandleNextMessage();
            }
            else if (Text.Length > Mathf.Min(3, messages[messagesIndex].Length))
            {
                ShowText(messages[messagesIndex]);
            }
        }
    }

    private void HandleNextMessage()
    {
        if (messagesIndex + 1 < messages.Length)
        {
            string nextMessage = messages[++messagesIndex];
            if (nextMessage.ToLower().StartsWith("message:"))
            {
                if (sender != null)
                {
                    Debug.Log("Calling: " + nextMessage.Substring(8, nextMessage.Length - 8));
                    sender.SendMessage(nextMessage.Substring(8, nextMessage.Length - 8));
                }

                HandleNextMessage();
            }
            else
            {
                ShowTextTyped(nextMessage, 0, typeDelay);
                isCurrentlyTyping = true;
            }
        }
        else
        {
            ClearText();
            messages = new string[] { };
            messagesIndex = 0;
            allowMovement = true;
            sender = null;
        }
    }

    public void ShowText(string text)
    {
        CancelInvoke();
        Text = text;
        background.enabled = true;
        isCurrentlyTyping = false;
    }

    public void ShowText(string text, float duration)
    {
        ShowText(text);
        Invoke("ClearText", duration);
        isCurrentlyTyping = false;
    }

    public void ShowTextTyped(string text, float duration, float typeDelay)
    {
        ShowText("");
        buffer = text;
        bufferIndex = 0;
        InvokeRepeating("TypeSingleLetterFromBuffer", 0, typeDelay);
        if (duration > 0)
            Invoke("ClearText", (text.Length + 1) * typeDelay + duration);
        isCurrentlyTyping = true;
    }

    /// <summary>
    /// Multi messages.
    /// </summary>
    public void ShowTextTyped(string[] text, float typeDelay, GameObject sender = null)
    {
        if (text.Length > 0)
            ShowText("");
            //ShowTextTyped(text[0], 0, typeDelay);

        this.sender = sender;
        allowMovement = false;
        messages = text;
        messagesIndex = -1;
        this.typeDelay = typeDelay;
        isCurrentlyTyping = false;
    }

    private void TypeSingleLetterFromBuffer()
    {
        if (bufferIndex < buffer.Length)
        {
            isCurrentlyTyping = true;
            Text = Text + buffer[bufferIndex++];
        }
        else
        {
            isCurrentlyTyping = false;
            CancelInvoke("TypeSingleLetterFromBuffer");
        }
    }

    private void ClearText()
    {
        Text = "";
        background.enabled = false;
        CancelInvoke();
        isCurrentlyTyping = false;
    }
}
