using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeButton : MonoBehaviour
{

    public float Ammount;
    private Coin parentCoin;
    public bool ALLsphaget;
    public bool Buy;
    // Use this for initialization
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        parentCoin = GetComponentInParent<Coin>();
    }

    private void OnClick()
    {

        float money = MoneyManager.Instance.Money;
        if (Buy)
        {
            if (parentCoin.Value == 0)
                return;
            if (!ALLsphaget)
            {
                if (money >= parentCoin.Value * Ammount)
                {
                    parentCoin.Ammount += Ammount;
                    MoneyManager.Instance.Money -= parentCoin.Value * Ammount;
                }
            }
            else if (ALLsphaget)
            {
                if (money >= parentCoin.Value)
                {
                    float allAmmount = money / parentCoin.Value;
                    parentCoin.Ammount += allAmmount;
                    MoneyManager.Instance.Money -= allAmmount * parentCoin.Value;
                }
            }
        }
        if (!Buy)
        {
            if (!ALLsphaget)
            {
                if (parentCoin.Ammount >= Ammount)
                {
                    parentCoin.Ammount -= Ammount;
                    MoneyManager.Instance.Money += parentCoin.Value * Ammount;
                }
            }
            else if (ALLsphaget)
            {
                MoneyManager.Instance.Money += parentCoin.Ammount * parentCoin.Value;
                parentCoin.Ammount -= parentCoin.Ammount;
            }
        }
    }
}
