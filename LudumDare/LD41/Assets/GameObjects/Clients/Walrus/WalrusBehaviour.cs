using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class WalrusBehaviour : ClientBehaviour
{
    protected override void Initialize()
    {
        nextVisit = FirstVisit;
        ai = this.StartCoroutineIfNotStarted(ref ai, AiLoop());
        ClientRulesManager.Instance.AddJoinRule<WalrusBehaviour>(() => HasRareItems() || HasEpicItems());
        AddConditionAndReaction("dragon's head", () => HasActiveNotHiddenItem("dragon's head"), DragonsHeadReaction);
    }

    protected override void Think()
    {
    }

    private IEnumerator FirstVisit()
    {
        yield return Say("Hello. I'm a collector from the west.");
        yield return Say("I'm interested in all kinds of <color=green>rare</color> and <color=purple>epic</color> items.");
        yield return Ask("Does your shop has anything interesting to offer?");
        yield return HandleItem();

        nextVisit = FurtherVisits;
    }

    private IEnumerator DragonsHeadReaction()
    {
        yield return Say("Oh wow, that's a red draong's head!");
        yield return Ask("I can give you 1000 gold for it, what do you say?");
        if (Item == null)
        {
            yield return Say("You won't find a better buyer, keep that in mind! *groan*");
        }
        else if (Item.Name == "dragon's head")
        {
            yield return SayPayLeave("Good business!", 1000);
        }
        else
        {
            yield return Say("I'm not interested in this now...");
        }

        nextVisit = FurtherVisits;
    }

    private IEnumerator FurtherVisits()
    {
        yield return Say("Hello, again. I need to expand my collection of <color=green>rare</color> and <color=purple>epic</color> items.");
        yield return Ask("Do you have anything new, rare and interesting?");
        yield return HandleItem();
    }

    private IEnumerator HandleItem()
    {
        yield return new WaitForFixedUpdate();
        if (Item == null)
        {
            yield return Say("Oh well, I'll visit you next time.");
            RemoveClientIfNoItems();
        }
        else if (Item.Type == "rare")
        {
            yield return Say($"Oh...  I see, it's a {Item.Name}. *whistle*");
            yield return SayPayLeave("They are quite rare indeed. I'll pay you 200 gold.", 3, 200);
        }
        else if (Item.Type == "epic")
        {
            yield return Say($"!!!");
            yield return Say($"It's the {Item.Name}");
            yield return Say($"How did you get this? There are only several of these in the whole world!");
            yield return SayPayLeave("1000 gold it is!", 3, 1000);
        }
        else
        {
            yield return Say("...");
            yield return Say("Do you really think I would be interested in THIS? *growl*");
            yield return SayPayLeave("I'm not paying for this.", 3, 0);
            RemoveClientIfNoItems();
        }
    }

    protected void RemoveClientIfNoItems()
    {
        if (!HasRareItems() && !HasEpicItems())
        {
            RemoveFromClients();
            ClientRulesManager.Instance.AddJoinRule<WalrusBehaviour>(() => HasRareItems() || HasEpicItems());
        }
    }
}
