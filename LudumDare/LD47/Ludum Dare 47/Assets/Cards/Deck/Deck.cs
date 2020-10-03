using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> AllCards;


    [ContextMenu("Reshuffle")]
    public void Reshuffle()
    {
        var cardHeight = new Vector3(0, 0, .03f);
        var index = 0;

        foreach (var card in FindObjectsOfType<Card>())
        {
            card.transform.position = transform.position + cardHeight * index++;
        }
    }
}
