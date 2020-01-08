using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class CowBehaviour : ClientBehaviour
{
    private readonly int staffPrice = 400;
    private readonly int metalPrice = 50;

    Coroutine ai;
    public GameObject[] ItemsForSale;

    protected override void Initialize()
    {
        nextVisit = SellSomeItem;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
    }

    protected override void Think()
    {
    }

    private IEnumerator SellSomeItem()
    {
        yield return Say("Hello...");

        if (Random.Range(0, 2) == 1)
        {
            yield return Say("I see big adventures in your future...");
            yield return Sell("This #name# could aid you very well.. And it's only #price# gold...",
            "Remember! With great power comes great responsibility!", 3,
            "I understand, not everyone wants to handle such power...", 3,
            ItemsForSale[0], staffPrice);
        }
        else
        {
            yield return Sell("Would you be interested in fine #name#? Only #price# gold...",
            "I hope you'll shape it into something beautiful...", 3,
            "If you change your mind, I'll be around...", 2,
            ItemsForSale[1], metalPrice);
        }

        yield return NotBoughtLogic();
    }

    private IEnumerator NotBoughtLogic()
    {
        if (!Bought)
        {
            RemoveFromClients();
            ClientRulesManager.Instance.AddJoinRule<CowBehaviour>(() => Chest.Instance.Gold >= staffPrice || (!HasAnyItemNamed("metal") && Chest.Instance.Gold >= metalPrice));
        }

        yield return null;
    }
}
