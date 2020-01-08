using DreamBit.Extensions;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LizwardBehaviour : ClientBehaviour
{
    public GameObject GlowingOrbPrefab;
    public GameObject PoseidonsCrownPrefab;

    protected override void Initialize()
    {
        nextVisit = FirstVisit;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
        AddConditionAndReaction("dragon's head", () => HasActiveNotHiddenItem("dragon's head"), DragonHeadReaction);
    }

    protected override void Think()
    {
    }

    private IEnumerator FirstVisit()
    {
        yield return Say("Hello! The name is Lizward *squeak* *squeak*");
        yield return Ask("I'm a great wizard but I get tired fast. Do you have something that could help? *squeak*");
        if (Item == null)
        {
            yield return SayPayLeave("I'll come next time if you think of anyting.", 0);
        }
        else if (Item.Name == "mana potion" || Item.Name == "apple" || Item.Name == "coffee")
        {
            yield return GoodReaction();
            nextVisit = ProgressedVisits;
        }
        else
        {
            yield return BadReaction();
        }
    }

    private IEnumerator ProgressedVisits()
    {
        if (Random.Range(0, 3) == 2 || HasAnyItemNamed("staff"))
        {
            yield return EpicMission();
        }
        else
        {
            yield return Say("Hello!");
            GiveItem(GlowingOrbPrefab);
            yield return Say("Some bandit dropped this while we were figthing. Let this be a gift to you!");
            yield return Ask($"Oh, right! I'm back for more of that stuff you sold me! *squeeek*");
            if (Item == null)
            {
                yield return SayPayLeave("I'll come next time when you have something.", 0);
            }
            else if (Item.Name == "mana potion" || Item.Name == "apple" || Item.Name == "coffee")
            {
                yield return GoodReaction();
            }
            else
            {
                yield return BadReaction();
            }
        }
    }

    private IEnumerator DragonHeadReaction()
    {
        yield return Say("...? *squeak squeak*");
        yield return Say("Is that a dragon's head?");
        yield return Say("That's outrageous!! *squeak*", 1.5f);
        yield return Say("Dragons are majestic creatues and should not be treated this way!");
        yield return Say("I'm not coming back here again!!! *sqeek*", 2f);
        RemoveFromClients();
    }

    private IEnumerator EpicMission()
    {
        yield return Say("Hello again..! *short squeak*");
        yield return Say("I want to go on an epic adventure to the city under the sea, but...");
        yield return Say("It's very dangerous! *squeek*");
        yield return Ask("Do you have any <color=purple>epic</color> items that could help me?");

        if (Item == null)
        {
            yield return SayPayLeave("It's ok. *squeek* I'll come back later.", 3, 0);
        }
        else if (Item.Type == "epic")
        {
            yield return Say($"Is... Is this {Item.Name}...?");
            yield return SayPayLeave("I'll surely succeed with this on me! Thank you!", 450);
            nextVisit = ReturnFromPoseidon;
        }
        else
        {
            yield return SayPayLeave("Oh... That's not... Maybe I'll go next time.", 3.5f, 0);
        }
    }

    private IEnumerator BadReaction()
    {
        yield return SayPayLeave("Are you sure this will help? Well... Ok, thank you... *click* *click*", 15);
    }

    private IEnumerator GoodReaction()
    {
        yield return SayPayLeave($"That's a nice looking {Item.Name}! *squeak*", 30);
    }

    private IEnumerator ReturnFromPoseidon()
    {
        yield return Say("Hello again! *squeek*");
        yield return Say($"I did it! *squeek*");
        yield return Say($"Right after you gave me the {OwnedItems.Last().Name} I rushed to the underwater city.");
        yield return Say("I found there Poseidon, the ruthless ruler who wishes no good upon his city's people.");
        yield return Say("*sqeeeek* This is when I challenged him!");
        yield return Say("We faught for hours.");
        yield return Say("I was beginning to lose hope, but then...");
        yield return Say("... The staff you gave me, it started glowing red hot. *squeek*");
        yield return Say("I rushed towards Poseidon with all I had left and impaled his heart.");
        yield return Say("*squeek* and here I am now!");
        yield return Say("The staff you gave me saved me!");
        yield return Say("As a thank you, I want to give you this");
        GiveItem(PoseidonsCrownPrefab);
        AchivementBadge.Achieved("poseidon");
        yield return Say("It's Poseidon's crown. Do what you want with it hehe. *sqeeek*");
        yield return Say("Bye and see ya! *squek*");

        RemoveFromClients();
    }
}
