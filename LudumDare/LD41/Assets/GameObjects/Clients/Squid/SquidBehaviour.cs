using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class SquidBehaviour : ClientBehaviour
{
    public GameObject[] dragonHunters;

    private void OnEnable()
    {
        Global.Dragon = null;
    }

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
        Global.Dragon = "active";
        yield return Say("Have you heard??", 1.5f);
        yield return Say("The great red dragon has awaken!", 3f);
        yield return Say("We are all dooomed...!!!", 2f);

        nextVisit = FurtherVisits;
    }

    private IEnumerator FurtherVisits()
    {
        if (Global.Dragon == "active")
        {
            yield return Say("It's terrible!", 2);
            yield return Say("Nearby city has been destroyed!", 3f);
            yield return Say("We are next...!", 2f);
            RemoveFromClients();
            ClientRulesManager.Instance.AddJoinRule<SquidBehaviour>(() => Global.Dragon == "defeated");
        }
        else if (Global.Dragon == "defeated")
        {
            yield return Say("It's a mirracle! The dragon was defeated!");
            yield return Say("The hero who slayed the dragon must be very strong...");
            RemoveFromClients();
        }
        else
        {
            RemoveFromClients();
        }
    }
}
