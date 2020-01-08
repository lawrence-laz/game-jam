using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public float agingSpeed;
    public CoinData data;
    public float currentValue;
    public float Value
    {
        get { return currentValue; }
        set
        {
            if (value >= currentValue && value != 0)
            {
                ValueText.color = Color.green;
                Arrow.color = Color.green;
                Arrow.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                ValueText.color = Color.red;
                Arrow.color = Color.red;
                Arrow.transform.localScale = new Vector3(1, -1, 1);
            }

            currentValue = value;
            string formatting = "0.#";
            if (currentValue > 100)
                formatting = "0";
            ValueText.text = currentValue.ToString(formatting) + "$";
        }
    }

    public string coinName;
    public string Name { get { return coinName; } set { coinName = value; NameText.text = value; } }
    private float ammount;
    public float Ammount { get { return ammount; } set { ammount = value; AmmountText.text = ammount.ToString("0"); } }
    public float MaxAmmount { get { return 9999; } }
    private float miningSpeed;
    public float MiningSpeed
    {
        get { return HardwareManager.GetFinalMineSpeed(this); }
        //set
        //{
        //    miningSpeed = value;
        //    Debug.Log(value);
        //    MiningSpeedText.text = "Mining speed: " + (((float)value) / HardwareManager.MinningTick).ToString("0.##") + " / s";
        //}
    }
    private float computationHardnessCoficient;
    public float ComputationHardnessCoficient { get { return computationHardnessCoficient; } set { computationHardnessCoficient = value; } }
    public bool IsMining { get; set; }
    

    public Text ValueText;
    public Text AmmountText;
    public Text NameText;
    public Image Icon;
    private Image Arrow;
    public Text MiningSpeedText;

    public static bool IsWorking(string name)
    {
        Coin coin = GetByName(name);
        return coin != null ? coin.enabled : false;
    }

    public static Coin GetByName(string name)
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coinObj in coins)
        {
            if (coinObj.GetComponent<Coin>().data.name == name ||
                coinObj.GetComponent<Coin>().data.coinName == name)
                return coinObj.GetComponent<Coin>();
        }

        return null;
    }

    public static List<Coin> GetAllActive()
    {
        GameObject[] coinsObj = GameObject.FindGameObjectsWithTag("Coin");
        List<Coin> coins = new List<Coin>();
        foreach (GameObject coinObj in coinsObj)
        {
            if (coinObj.GetComponent<Coin>().Value > 0)
                coins.Add(coinObj.GetComponent<Coin>());
        }

        return coins;
    }

    public static List<Coin> GetActivelyMining()
    {
        GameObject[] coinsObj = GameObject.FindGameObjectsWithTag("Coin");
        List<Coin> coins = new List<Coin>();
        foreach (GameObject coinObj in coinsObj)
        {
            if (coinObj.GetComponent<Coin>().IsMining)
                coins.Add(coinObj.GetComponent<Coin>());
        }

        return coins;
    }

    private void OnEnable()
    {
        Arrow = transform.Find("Image").GetComponent<Image>();
        computationHardnessCoficient = 1;
    }

    void Start()
    {
        NameText.text = data.coinName;
        Value = data.StartingValue;
        Icon.sprite = data.IconSprite;
        CancelInvoke();
        InvokeRepeating("UpdateMinerText", 2, 2);
    }

    private void UpdateMinerText()
    {
        MiningSpeedText.text = "Mining speed: " + MiningSpeed.ToString("0.##") + " / s";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
