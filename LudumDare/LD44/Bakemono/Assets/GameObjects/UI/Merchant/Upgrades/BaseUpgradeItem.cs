using DG.Tweening;
using System;
using UnityEngine;

public class BaseUpgradeItem : MonoBehaviour
{
    public int Price;

    public void BuyLogic(Action<HeroStats> upgradeImplementation)
    {
        var stats = HeroStats.Get();
        if (stats.Health < Price)
        {
            Debug.LogWarning("Insufficient souls." + stats.Health + "/" + Price);
        }

        Destroy(GameObject.Find("MerchantWindow_Object"));
        HeroStats.Get().GetComponent<PlayerMove>().enabled = false;
        GameObject.Find("MerchantBuy_Audio").GetComponent<AudioSource>().Play();
        ChangeMerchantSprite("MerchantHands_Sprite", true);
        ChangeMerchantSprite("MerchantSit_Sprite", false);

        DOTween.Sequence()
            .AppendInterval(1.5f)
            .AppendCallback(() =>
            {
                if (stats.Health <= Price)
                {
                    stats.DirectHealth = 20;
                }
                else
                {
                    stats.DirectHealth = Mathf.Min(stats.DirectHealth - Price, 50);
                }
            })
            .AppendInterval(1f)
            .AppendCallback(() =>
            {
                upgradeImplementation.Invoke(stats);
                var playerObj = HeroStats.Get();
                if (playerObj != null)
                {
                    var move = playerObj.GetComponent<PlayerMove>();
                    if (move != null)
                    {
                        move.enabled = true;
                    }

                    ChangeMerchantSprite("MerchantSit_Sprite", true);
                    ChangeMerchantSprite("MerchantHands_Sprite", false);
                }
            });
    }

    private void ChangeMerchantSprite(string to, bool enabled)
    {
        GameObject.Find("Merchant_Object").transform.Find(to).gameObject.SetActive(enabled);
    }
}
