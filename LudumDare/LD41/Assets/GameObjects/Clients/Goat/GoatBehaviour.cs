using Assets.External.DreamBit.Extension;
using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class GoatBehaviour : ClientBehaviour
{
    private readonly int PotionPrice = 40;
    private readonly int HealthPotionIndex = 0;
    private readonly int ManaPotionIndex = 1;

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
        yield return Say("Good day *baa*");

        yield return Sell("Perhaps you would care to restock on #name#? That would be #price# gold",
            "I'm sure we'll be seing each other again soon *baa*", 3,
            "You might find yourself regretting this decision... *baaa*", 2,
            GetItemToSell(), PotionPrice);

        yield return NotBoughtLogic();
    }

    private GameObject GetItemToSell()
    {
        if (!HasAnyItemNamed("mana potion"))
        {
            return ItemsForSale[ManaPotionIndex];
        }
        else if (!HasAnyItemNamed("health potion"))
        {
            return ItemsForSale[HealthPotionIndex];
        }
        else
        {
            return ItemsForSale.GetRandom();
        }
    }

    private IEnumerator NotBoughtLogic()
    {
        if (!Bought)
        {
            RemoveFromClients();
            ClientRulesManager.Instance.AddJoinRule<GoatBehaviour>(() => !HasAnyItemNamed("mana potion") || !HasAnyItemNamed("health potion"));
        }

        yield return null;
    }
}
