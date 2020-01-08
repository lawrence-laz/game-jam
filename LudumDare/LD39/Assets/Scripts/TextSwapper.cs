using UnityEngine;
using System.Collections.Generic;

public class TextSwapper : MonoBehaviour
{
    [SerializeField]
    public List<ListOfStrings> listOfMessages;
    int index;

    TextTrigger textTrigger;
    Portal portal;

    private void Start()
    {
        portal = GetComponent<Portal>();
        textTrigger = GetComponent<TextTrigger>();
        index = 0;
    }

    public void SwapText()
    {
        if (index >= listOfMessages.Count
            || (portal == null && textTrigger == null))
            return;

        string[] nextMessages = listOfMessages[index].List.ToArray();
        if (portal != null)
        {
            portal.lockedMessage = nextMessages;
        }
        else
        {
            textTrigger.messages = nextMessages;
        }
        ++index;
    }
}
