using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardwareManager : MonoBehaviour
{
    public double cash;
    public Text upgradeButtonText;
    public Text computationPowerText;
    public Coin coin;
    public GameObject miningTextSpawn;
    public Button upgradeButton;

    public static bool IsInWinCondition = false;
    public Image hardwareImage;
    public Image backgroudImage;
    public Sprite[] hardwareImages = new Sprite[17];
    public PostImage[] endPostsImage;
    public PostText[] endPostsText;
    public Event endEvent;

    private Hardware[] hardwares;
    private int hardwareLevel = 0;

    public const float MinningTick = 3f;
    public Text Name;
    // Use this for initialization
    void Start()
    {
        IsInWinCondition = false;
        hardwares = new Hardware[8];
        //hardwares[0] = new Hardware(0, 20, hardwareImages[0]);
        //int speedup = 0;
        //for (int i = 1; i < 8; i++)
        //{
        //    hardwares[i] = new Hardware((i * 4) + speedup * 2, (i * 250) + speedup * 300, hardwareImages[i]);
        //    speedup++;
        //}
        hardwares[0] = new Hardware(0, 20, hardwareImages[0],"None");
        hardwares[1] = new Hardware(1, 150, hardwareImages[1],"Potato");
        hardwares[2] = new Hardware(5, 250, hardwareImages[2],"CPU");
        hardwares[3] = new Hardware(15, 1000, hardwareImages[3],"GPU");
        hardwares[4] = new Hardware(50, 10000, hardwareImages[4],"Multi GPU");
        hardwares[5] = new Hardware(100, 25000, hardwareImages[5], "ASIC");
        hardwares[6] = new Hardware(400, 100000, hardwareImages[6], "Cloud™");
        hardwares[7] = new Hardware(999999, 500000, hardwareImages[7],"Quantum Box");

        LoadHardware();

        InvokeRepeating("MiningCoroutine", MinningTick, MinningTick);
    }

    public void MiningCoroutine()
    {
        List<Coin> activeCoins = Coin.GetActivelyMining();

        foreach (Coin coin in activeCoins)
        {
            float coinsMined;
            //if (coin.AmmountMined == 0)
            //    coinsMined = hardwares[hardwareLevel].computationPower / (1 + coin.ComputationHardnessCoficient) / activeCoins.Count;
            //else
            coinsMined = GetMineSpeed(coin);
            coin.Ammount += coinsMined;
            //coin.MiningSpeed = coinsMined;
        }
    }

    public float GetMineSpeed(Coin coin)
    {
        return coin.IsMining ? hardwares[hardwareLevel].computationPower / (1 + Time.time * 0.1f + coin.agingSpeed) / Coin.GetActivelyMining().Count : 0;
    }

    public static float GetFinalMineSpeed(Coin coin)
    {
        HardwareManager[] miners = GameObject.Find("HardwareUI").GetComponentsInChildren<HardwareManager>();
        float finalValue = 0;
        foreach (HardwareManager miner in miners)
            finalValue += miner.GetMineSpeed(coin);

        return finalValue;
    }

    public void OnUpgradeButtonClick()
    {
        if (MoneyManager.Instance.Money < hardwares[hardwareLevel].upgradePrice)
            return;

        MoneyManager.Instance.Money -= hardwares[hardwareLevel].upgradePrice;
        hardwareLevel++;

        LoadHardware();

        if (hardwareLevel == 7)
        {
            upgradeButton.interactable = false;
            StartCoroutine(EndGameLogic());
        }
    }

    private IEnumerator EndGameLogic()
    {
        IsInWinCondition = true;
        EventManager.Instance.PerformEvent(endEvent);

        foreach (PostText post in endPostsText)
        {
            Feed.Instance.Post(post);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }

        foreach (PostImage post in endPostsImage)
        {
            Feed.Instance.Post(post);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }

    void LoadHardware()
    {
        computationPowerText.text = "Computation power \n" + hardwares[hardwareLevel].computationPower;
        upgradeButtonText.text = "Upgrade \n" + hardwares[hardwareLevel].upgradePrice + "$";
        Name.text = hardwares[hardwareLevel].Name;
        hardwareImage.sprite = hardwares[hardwareLevel].sprite;

        if (hardwareLevel == 7)
            upgradeButtonText.text = "Impossibru";
    }
}
