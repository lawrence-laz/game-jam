using Assets.External.DreamBit.Extension;
using DreamBit.Extensions;
using System.Collections;
using UnityEngine;
using DG.Tweening;

// TODO
/// <summary>
/// Bat required some condition to turn into a vampire.
/// Bat is afraid of glowing orb, it burns her eyes.
/// Bat is interested in health potions and beutiful things?
/// </summary>
public class BatBehaviour : ClientBehaviour
{
    public GameObject[] GoodItems;
    public GameObject[] BadItems;
    public GameObject[] ItemsForSale;

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

    private Coroutine ai;
    private ParticleSystem particles;
    private GameObject batSprite;
    private GameObject vampireSprite;

    protected override void Initialize()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        batSprite = transform.Find("Bat_Sprite").gameObject;
        batSprite.transform.FlipHorizontal();
        vampireSprite = transform.Find("Vampire_Sprite").gameObject;
        vampireSprite.transform.localScale = Vector3.one * 0.5f;
        nextVisit = FirstTimeHello;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
    }

    protected override void Think()
    {
    }

    protected override IEnumerator BeforeEnter()
    {
        batSprite.transform.FlipHorizontal();
        yield return null;
    }


    private IEnumerator TurnIntoBat()
    {
        particles.Play();
        batSprite.SetActive(true);
        vampireSprite.transform.DOScale(0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        vampireSprite.SetActive(false);
        batSprite.transform.FlipHorizontal();
    }

    private void TurnIntoVampire()
    {
        particles.Play();
        batSprite.SetActive(false);
        vampireSprite.transform.DOScale(2f, 0.3f);
        vampireSprite.SetActive(true);
    }

    private IEnumerator FirstTimeHello()
    {
        TurnIntoVampire();

        yield return Say("Hello there...");

        yield return TurnIntoBat();
    }
}
