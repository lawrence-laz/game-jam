using DreamBit.Extensions;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DuckerWarriorBehaviour : ClientBehaviour
{
    public GameObject DragonHeadPrefab;

    Coroutine ai;
    protected override void Initialize()
    {
        nextVisit = AskForGearFirstTime;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
        ClientRulesManager.Instance.AddJoinRule<DuckerWarriorBehaviour>(() => Global.Dragon == "active");
    }

    protected override void Think()
    {
    }

    private IEnumerator AskForGearFirstTime()
    {
        yield return Say("Hi. My name is Ducker and I'm a warrior. *quack*");
        yield return Say("I could beat a dragon any day, you know. I just don't have the gear...");
        yield return Say("... and I don't have any money to buy it with. So...");
        yield return Ask("Could you lend me something good? I promise to return it when I beat a dragon.");

        yield return HandleItem();

    }

    private IEnumerator AskForGearAgain()
    {

        yield return Say("Hi, again. *quack*");
        if (OwnedItems.Count > 0)
            yield return Say($"Uhm... A {OwnedItems.Last().Name} wasn't enough to beat a dragon.");
        else
            yield return Say($"I really need better items.");
        yield return Say("Almost got myself killed fighting dragon *quack*");
        yield return Ask("Can you give me some better gear? I promise to slay a dragon.");

        yield return HandleItem();
    }

    private IEnumerator HandleItem()
    {
        if (Item == null)
        {
            yield return SayPayLeave($"It's ok. I'll keep looking. I know I can do it.", 0);

            RemoveFromClients();
            ClientRulesManager.Instance.AddJoinRule<DuckerWarriorBehaviour>(() => HasRareItems());
            nextVisit = AskForGearAgain;
        }
        else if (Item.Type == "rare" || Item.Type == "epic")
        {
            yield return Say($"Yes! A {Item.Name} is exactly what I needed!");
            yield return SayPayLeave("That dragon doesn't stand a chance. See ya later *quack*", 4, 0);
            nextVisit = GiveDragonHead;
        }
        else
        {
            yield return SayPayLeave($"A {Item.Name} isn't a very good gear. But I'll try.", 4, 0);
            RemoveFromClients();
            ClientRulesManager.Instance.AddJoinRule<DuckerWarriorBehaviour>(() => HasRareItems());
            nextVisit = AskForGearAgain;
        }
    }

    private IEnumerator GiveDragonHead()
    {
        Global.Dragon = "defeated";
        yield return Say("Hi, me again! Guess what...");
        AchivementBadge.Achieved("dragon");
        yield return Say("I slayed the dragon! But uhm...");
        yield return Say($"The {OwnedItems.Last().Name} was broken in the process *quack*");
        yield return Say($"I got you dragon's head though! So no hard feelings ok?");
        GiveItem(DragonHeadPrefab);
        yield return Say("Bye and sorry again for the item.", 3);
        RemoveFromClients();
    }
}
