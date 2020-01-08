using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class DolarCounterManager : MonoBehaviour
{
    public Text ownedDolarsText;


    void Update()
    {
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";

        ownedDolarsText.text = MoneyManager.Instance.Money.ToString("0#.", nfi) + " $";
    }
}
