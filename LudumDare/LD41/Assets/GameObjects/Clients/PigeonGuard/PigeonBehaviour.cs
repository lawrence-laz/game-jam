using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class PigeonBehaviour : ClientBehaviour
{
    public GameObject BanditLordSwordPrefab;

    Coroutine ai;
    protected override void Initialize()
    {
        nextVisit = FirstVisit;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
    }

    protected override void Think()
    {
    }

    private IEnumerator FirstVisit()
    {
        yield return Say("Greetings, shop keeper! Royal guards are in trouble and we need your help!");
        yield return Say("Due to recent increases in bandit activity we are experiencing fund shortage.");
        yield return Ask("Any support is appreciated!");

        if (Item == null)
        {
            yield return Say($"It's very important to fight bandits. We hope for you support next time.");
            yield break;
        }
        else if (Item.Name == "vase with flowers")
        {
            yield return Say($"Uhm...");
            yield return Say($"Sorry, but I don't see how a {Item.Name} will help to fight bandits. *prrpt*");
            yield break;
        }

        switch (Item.Type)
        {
            case "crap":
                yield return Say("*prrrrpt prrrpt*", 2);
                yield return Say("Such disrespect!! *prrrpt*", 2);
                RemoveFromClients();
                yield return SayPayLeave("Kingdom will not have any further business with this shop.", 4, 0);
                break;
            case "common":
                yield return SayPayLeave("That brings us one step closer to the brighter future.", 0);
                // TODO: Make it so that bandit doesn't come for some turns.
                nextVisit = SecondVisit;
                break;
            default:
                yield return SayPayLeave("Kingdom is most grateful. Your generosity will not be forgotten!", 0);
                nextVisit = BanditLordDefeated;
                break;
        }
    }

    private IEnumerator SecondVisit()
    {
        yield return Say("Greetings again, keeper!");
        yield return Say("Due to your generous contributions Royal Guards are now close to defeating bandits!");
        yield return Say("Nevertheless...");
        yield return Ask("Battle's not over just yet! Any donations are appreciated.");

        if (Item == null)
        {
            yield return Say($"It's very important to fight bandits. We hope for you support next time.");
            Leave();
            yield break;
        }
        else if (Item.Name == "vase with flowers")
        {
            yield return Say($"Uhm...");
            yield return Say($"Sorry, but I don't see how a {Item.Name} will help to fight bandits. *prrpt*");
            Leave();
            yield break;
        }

        switch (Item.Type)
        {
            case "crap":
                yield return Say("*prrrrpt prrrpt*", 2);
                yield return Say("Such disrespect!! *prrrpt*", 2);
                RemoveFromClients();
                yield return SayPayLeave("Kingdom will not have any further business with this shop.", 4, 0);
                break;
            case "common":
                yield return SayPayLeave("This will aid us well!", 0);
                nextVisit = BanditLordDefeated;
                break;
            default:
                yield return SayPayLeave("Kingdom is most grateful. Your generosity will not be forgotten!", 0);
                nextVisit = BanditLordDefeated;
                break;
        }
    }

    private IEnumerator BanditLordDefeated()
    {
        RemoveFromClients<ThiefBehaviour>();
        yield return Say("Greetings!");
        AchivementBadge.Achieved("bandit");
        yield return Say("Kingdom has defeated the bandit lord.");
        yield return Say("As a thank you for your support kingdom would like to give you bandit lord's sword.");
        GiveItem(BanditLordSwordPrefab);

        nextVisit = BuySuppliesAfterDefeatingBanditLord;
    }

    int visitsForSuppliedAfterBandit = 0;
    private IEnumerator BuySuppliesAfterDefeatingBanditLord()
    {
        yield return Ask("Greetings. Royal guards would like to buy supplies.");
        if (Item == null)
            yield return Say("You don't have anything? Oh.. Well.. We will return next day!");
        else if (Item.Type == "crap")
            yield return SayPayLeave("*prrrpt* This is not what we meant!", 3, 0);
        else
            yield return SayPayLeave("Pleasure to have business with you.", 50);
        visitsForSuppliedAfterBandit++;

        if (visitsForSuppliedAfterBandit == 3)
            RemoveFromClients();
    }
}
