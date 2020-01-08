using Assets.External.DreamBit.Extension;
using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class ChickenboyBehaviour : ClientBehaviour
{
    Coroutine ai;
    public GameObject[] ItemsForSale;
    public Sprite CrownedChickenSprite;

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
        GameObject itemForSale = ItemsForSale.GetRandom();
        yield return SellOrReactToItem("Hello, sir.. Would you like to buy this #name# for #price# gold?",
            "Thank you!", 1.5f,
            "Oh no...", 2,
            itemForSale, 10,
            "", 0, true, true);

        if (Item != null)
        {
            if (Item.Name.Contains("crown"))
            {
                GetComponentInChildren<SpriteRenderer>().sprite = CrownedChickenSprite;
                yield return Say("This is the best gift I have ever received!");
                yield return Say("I'm so happy! *chirp chirp*");
            }
            else
            {
                yield return Say("Thank you for the gift!!");
            }
        }
    }
}
