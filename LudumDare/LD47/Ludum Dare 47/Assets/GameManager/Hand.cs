using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform[] Slots;

    public bool HasEmptySlot()
    {
        return Slots.Any(x => x.childCount == 0);
    }

    public int GetCardCount()
    {
        return Slots.Count(x => x.childCount != 0);
    }

    public IEnumerable<Card> GetCards()
    {
        foreach (var slot in Slots)
        {
            var card = slot.GetComponentInChildren<Card>();
            if (card != default)
            {
                yield return card;
            }
        }
    }

    public Tween AddToHand(Transform card)
    {
        foreach (var slot in Slots)
        {
            if (slot.childCount == 0)
            {
                card.SetParent(slot, true);

                return DOTween.Sequence()
                    .Append(card.DOLocalMove(Vector3.zero, 0.5f))
                    .Join(card.DOLocalRotateQuaternion(Quaternion.identity, 0.3f));
            }
        }

        Debug.LogWarning("Unable to draw card, to empty slots.");
        return null;
    }

    public void RemoveFromHand(Transform transform)
    {
        foreach (var slot in Slots)
        {
            if (transform.parent == slot)
            {
                transform.parent = null;
                
                return;
            }
        }

        Debug.LogWarning($"Object '{transform.gameObject.name}' was not in hand, couldn't remove.");
    }

    public Tween MakeSure2Cards()
    {
        var cardsToDraw = 2 - GetCardCount();

        if (cardsToDraw <= 0)
        {
            return null;
        }

        var sequence = DOTween.Sequence()
            .AppendInterval(0.1f);
        var deck = FindObjectOfType<Deck>();

        for (int i = 0; i < cardsToDraw; i++)
        {
            sequence.Append(deck.DrawCard());
        }

        Card.Animation = sequence;

        return sequence.Play();
    }

    public Tween HideHand()
    {
        return transform.DOMove(Vector3.down * 2, 0.7f).SetRelative(true);
    }

    public Tween ShowHand()
    {
        return transform.DOMove(new Vector3(0, -0.163f, -7.42f), 0.7f).SetRelative(false);
    }

    private void Start()
    {
        MakeSure2Cards();
    }
}
