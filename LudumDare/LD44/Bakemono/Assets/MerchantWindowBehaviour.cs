using System;
using UnityEngine;

public class MerchantWindowBehaviour : MonoBehaviour
{
    private void Start()
    {
        var obj1 = transform.GetChild(0).Find(GoToNextLevel.NextLevel.ToString() + "_Choice1_Object");
        if (obj1 != null)
        {
            obj1.gameObject.SetActive(true);
        }
        var obj2 = transform.GetChild(0).Find(GoToNextLevel.NextLevel.ToString() + "_Choice2_Object");
        if (obj2 != null)
        {
            obj2.gameObject.SetActive(true);
        }
    }

    public void EnableHeroShooting(bool enable)
    {
        try
        {
            GameObject.FindWithTag("Player")
            .GetComponentInChildren<ArrowShoot>()
            .enabled = enable;
        }
        catch(Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }
}
