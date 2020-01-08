using Assets.External.DreamBit.Extension;
using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class BlacksmithBehaviour : ClientBehaviour
{
    Coroutine ai;
    public GameObject[] SimpleRareWeapons;
    public GameObject[] GoldenItemToSell;

    protected override void Initialize()
    {
        nextVisit = BuyMetal;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
    }

    protected override void Think()
    {
    }

    private IEnumerator SellWeapon()
    {
        yield return Say("Hello!", 0.8f);
        GameObject itemForSale = SimpleRareWeapons.GetRandom();
        yield return Sell("I made this #name#, do you want to buy it for #price# gold?",
            "Aha! Good choice!", 2,
            "Fine, I'll come next time!", 0,
            itemForSale, 100);

        if (Bought)
            nextVisit = BuyMetal;
    }


    private IEnumerator SellGoldenItem()
    {
        yield return Say("Hello!", 0.8f);
        GameObject itemForSale = GoldenItemToSell.GetRandom();
        yield return Say("I combined the the gold you've sold me with the ruby I've found in the mines...");
        yield return Sell("... and I made this #name#, do you want to buy it for #price# gold?",
            "Aha! Good choice!", 2,
            "I'll look for other buyers then. Bye!", 0,
            itemForSale, 300);

        if (Bought)
            nextVisit = BuyMetal;
    }

    private IEnumerator BuyMetal()
    {
        yield return Say(IsFirstVisit ? "Hello! People call me Whitey. I'm a blacksmith." : "Hello once again!");

        Debug.Log("2");
        yield return Ask("I'm looking for interesting metals to make weapons of. Do you have any to sell?");

        if (Item == null)
        {
            yield return Say("Oh...", 1);
            yield return AskForLight();
        }
        else
        {
            if (Item.Tags.Contains("metal"))
            {
                if (Item.Tags.Contains("gold"))
                {
                    yield return SayPayLeave("It's been a long time since I made something out of gold. Thank you!", 200);
                    nextVisit = SellGoldenItem;
                }
                else
                {
                    yield return SayPayLeave("Thank you! I'll make something great out of this.", 50);
                    nextVisit = SellWeapon;
                }
            }
            else
            {
                yield return Say($"You don't know what metal is? This is a {Item.Name}, not metal.");
            }
        }
    }

    private IEnumerator AskForLight()
    {
        yield return Say("I could make metal myself, but it's too dark in the mines.");
        yield return Ask("Do you have a light source to sell?");

        if (Item == null)
        {
            yield return Say("Oh well, I'll try next time.", 1);

            RemoveFromClients();
            ClientRulesManager.Instance.AddJoinRule<BlacksmithBehaviour>(() => HasAnyItemTagged("metal", "light"));
        }
        else
        {
            if (Item.Name.ContainsAny("candle", "glow"))
            {
                yield return SayPayLeave("Great! I'll go to the mines right away.", 50);
                nextVisit = BuyHealthPotion;
            }
            else
            {
                yield return Say($"I don't think I'll be able to create light with a {Item.Name}...");
            }
        }
    }

    private IEnumerator BuyHealthPotion()
    {
        yield return Say("Those spiders in mines sure are big!");
        yield return Ask("Do you have a health potion to sell?");

        if (Item == null)
        {
            yield return Say("Oh well, I'll try next time.", 1);
        }
        else
        {
            if (Item.Name.ContainsAny("health", "apple"))
            {
                yield return SayPayLeave($"I feel better already!", 50);
                nextVisit = SellWeapon;
            }
            else
            {
                yield return Say($"{Item.Name} won't heal a spider bite...");
            }
        }
    }
}
