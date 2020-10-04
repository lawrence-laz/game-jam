using DG.Tweening;
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
            DOTween.Kill(card.transform);
            card.transform.parent = null;
            card.transform.position = transform.position - cardHeight * index++;
            card.transform.rotation = Quaternion.Euler(Vector3.up * 180 + Vector3.forward * Random.Range(-3f, 3f));
        }
    }

    [ContextMenu("Draw card")]
    public Tween DrawCard()
    {
        if (_currentDeck.Count == 0)
        {
            return null;
        }

        var hand = FindObjectOfType<Hand>();

        if (hand.HasEmptySlot())
        {
            if (hand.GetCardCount() == 2)
            {
                var stats = FindObjectOfType<Stats>();
                DOTween.To(() => stats.Fun, x => stats.Fun = x, -1f / 3, 0.2f)
                    .SetRelative(true);
            }

            var card = GetNextCard();

            return hand.AddToHand(card.transform);
        }

        return null;
    }

    // Just physically into a pile
    public void PutCardInDeck(GameObject card)
    {
        card.transform.parent = null;
        card.transform.position = transform.position;
        card.transform.rotation = Quaternion.Euler(Vector3.up * 180 + Vector3.forward * Random.Range(-3f, 3f));
    }

    public void ReshuffleCardsNotInHands()
    {
        var hand = FindObjectOfType<Hand>();
        var cardsInHand = hand.GetCards().ToList();
        var cards = FindObjectsOfType<Card>()
            .Where(x => !cardsInHand.Contains(x));
        var index = 0;
        var cardHeight = new Vector3(0, 0, .035f);

        foreach (var card in cards)
        {
            _currentDeck.Push(card.gameObject);
            card.transform.parent = null;
            card.transform.position = transform.position - cardHeight * index++;
            card.transform.rotation = Quaternion.Euler(Vector3.up * 180 + Vector3.forward * Random.Range(-3f, 3f));
        }
    }

    private GameObject GetNextCard()
    {
        GameObject card = default;
        while (card == default)
        {
            card = _currentDeck.Pop();

            if (_currentDeck.Count == 0)
            {
                ReshuffleCardsNotInHands();
            }

            if (card.GetComponent<WorkCard>() != null && (!FindObjectOfType<Clock>().IsWorkHours || FindObjectOfType<Calendar>().IsWeekend))
            {
                Debug.Log("Next card would have been go to work, but it's not work hours anymore or weekend.");
                card.transform.position = new Vector3(100, 100, 0);
                card = default;
            }
        }

        if (_currentDeck.Count == 0)
        {
            ReshuffleCardsNotInHands();
        }

        return card;
    }

    private void Start()
    {
        Reshuffle();
    }
}
