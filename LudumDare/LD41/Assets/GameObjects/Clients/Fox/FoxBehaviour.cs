using Assets.External.DreamBit.Extension;
using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

// TODO
/// <summary>
/// 5~ times doing alchemy gives an achievement
/// 
/// Needs more items 
/// more luck or interesting outcomes.
/// 
/// other ideas, maybe some items should have predefined probability?
/// Maybe some bad effect could be an item that explodes?
/// maybe a system where you can increase common items to rares, and rares to epics, or on bad luck downgrade by one?
/// 
/// maybe only metal can be turned into gold.
/// 
/// alchemy particle effect?
/// </summary>
public class FoxBehaviour : ClientBehaviour
{
    public GameObject[] GoodItems;
    public GameObject[] BadItems;

    private string[] hellos =
    {
        "Hello there, keeper...", "Ah! Fancy seeing you again... *grin*"
    };
    private string[] suggestions =
    {
        "Would you like to perform some alchemy..? Just give me an item...", "Care to test your luck? Just give me an item and leave the rest to alchemy..."
    };

    private readonly int PotionPrice = 40;
    private readonly int HealthPotionIndex = 0;
    private readonly int ManaPotionIndex = 1;

    public GameObject[] ItemsForSale;

    protected override void Initialize()
    {
        nextVisit = FirstTimeHello;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
    }

    protected override void Think()
    {
    }

    private IEnumerator FirstTimeHello()
    {
        yield return Say("Hello there...");
        yield return Say("My name is Slify. I'm studying the mystical art of alchemy. *grin*");
        yield return Say("Through its magical power I can turn one item into another.");

        yield return TryDoingAlchemy();
    }


    private IEnumerator OtherTimesHello()
    {
        yield return Say(hellos.GetRandom());

        yield return TryDoingAlchemy();
    }

    private IEnumerator TryDoingAlchemy()
    {
        yield return Ask(suggestions.GetRandom());
        if (Item == null)
        {
            yield return Say("If you change your mind... Let me know, ok..? *grin*");
        }
        else
        {
            yield return Say("Let's see...", 2f);

            Random.InitState((int)Time.time);
            if (Random.value < 0.6f)
                yield return GoodAlchemyAttempt();
            else
                yield return BadAlchemyAttempt();
        }

        nextVisit = OtherTimesHello;
    }

    private IEnumerator GoodAlchemyAttempt()
    {
        yield return Say("...", 2f);
        yield return Say("Would you look at that...!", 1);
        yield return GiveItem(GoodItems.GetRandom());
        yield return Say("This is the greatness of alchemy! *grin*");
        yield return Say("I'll be seeing you next time...");
    }

    private IEnumerator BadAlchemyAttempt()
    {
        yield return Say("...", 3f);
        yield return GiveItem(BadItems.GetRandom());
        yield return Say("Hm... I guess alchemy has its ways and reasons...");
        yield return Say("We'll have better luck next time..! I can feel it. *grin*", 3f);
    }
}
