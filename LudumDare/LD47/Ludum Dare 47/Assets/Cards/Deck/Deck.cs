using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> AllCards;

    private Stack<GameObject> _currentDeck = new Stack<GameObject>();


    [ContextMenu("Reshuffle")]
    public void Reshuffle()
    {
        var cardHeight = new Vector3(0, 0, .035f);
        var index = 0;

        _currentDeck.Clear();

        foreach (var card in FindObjectsOfType<Card>().OrderBy(_ => Random.value))
        {
            _currentDeck.Push(card.gameObject);
            card.transform.parent = null;
            card.transform.position = transform.position - cardHeight * index++;
            card.transform.rotation = Quaternion.Euler(Vector3.up * 180 + Vector3.forward * Random.Range(-3f, 3f));
        }
    }

    [ContextMenu("Draw card")]
    public void DrawCard()
    {
        if (_currentDeck.Count == 0)
        {
            return;
        }

        var hand = FindObjectOfType<Hand>();

        if (hand.HasEmptySlot())
        {
            var card = _currentDeck.Pop();
            hand.AddToHand(card.transform);
        }
    }

    private void Start()
    {
        Reshuffle();
    }
}
