using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> AllCards;

    private List<GameObject> _currentDeck = new List<GameObject>();

    [ContextMenu("Reshuffle")]
    public void Reshuffle()
    {
        var cardHeight = new Vector3(0, 0, .035f);
        var index = 0;

        _currentDeck.Clear();
        foreach (var card in FindObjectsOfType<Card>().OrderBy(_ => Random.value))
        {
            _currentDeck.Add(card.gameObject);
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

        if (Work.IsWorkTime()
            && !FindObjectOfType<OtherEffects>().WorkdAccountedForToday
            && Enumerable.Reverse(_currentDeck).Take(3).All(x => x.GetComponent<WorkCard>() == null)
            && FindObjectOfType<Hand>().GetCards().All(x => x.GetComponent<WorkCard>() == null))
        {
            var workCardIndex = _currentDeck.FindIndex(gameObject => gameObject.GetComponent<WorkCard>() != null);
            if (workCardIndex != -1)
            {
                Debug.Log("Good ol switcheroo");

                var newIndex = Mathf.Clamp(_currentDeck.Count - Random.Range(1, 4), 0, _currentDeck.Count - 1);
                var temp = _currentDeck[workCardIndex];
                _currentDeck[workCardIndex] = _currentDeck[newIndex];
                _currentDeck[newIndex] = temp;
            }
            else
            {
                Debug.Log("That's strange. Added work card to top.");
                _currentDeck.Add(FindObjectOfType<WorkCard>().gameObject);
            }
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
            _currentDeck.Add(card.gameObject);
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
            card = _currentDeck.Last();
            _currentDeck.RemoveAt(_currentDeck.Count - 1);

            if (_currentDeck.Count == 0)
            {
                ReshuffleCardsNotInHands();
            }

            if (card.GetComponent<WorkCard>() != null && !Work.IsWorkTime())
            {
                Debug.Log($"Next card would have been go to work, but it's not work hours anymore or weekend ({FindObjectOfType<Clock>().Time}).");
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
