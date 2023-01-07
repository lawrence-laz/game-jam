using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label : MonoBehaviour
{
    public string Text = "???";

    public bool Is(string text)
    {
        var thisText = Text.StartsWith("a ")
            ? Text[2..]
            : Text.StartsWith("an ")
            ? Text[3..]
            : Text;

        return string.Equals(thisText, text, System.StringComparison.InvariantCultureIgnoreCase);
    }
}
