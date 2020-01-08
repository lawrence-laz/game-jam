using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class ThiefBehaviour : ClientBehaviour
{
    Coroutine ai;
    protected override void Initialize()
    {
        nextVisit = StealVisit;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
    }

    protected override void Think()
    {
    }

    private IEnumerator StealVisit()
    {
        yield return Say("This is a robbery! Handover your most expensive item! I'll know if you're lying!", 1);
        yield return WaitForItem();

        if (Item.Name.Contains("spider"))
        {
            yield return Say("Aaaah!!!", 1);
        }
        else if (Item.Type == "common")
        {
            yield return Say("Are you kidding?", 1.5f);
            yield return Say($"This {Item.Name} isn't worth anything...", 3f);
            yield return SayPayLeave("I'll take some gold as well. Sayonara! *meoooow*", 3, -Random.Range(10, 45));
        }
        else
        {
            yield return SayPayLeave($"A {Item.Name}? Jackpot! *meoooow*", 3, 0);
        }
    }
}
