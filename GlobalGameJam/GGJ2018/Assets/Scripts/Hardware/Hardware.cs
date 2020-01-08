using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Hardware
{
    public float computationPower;
    public float upgradePrice;
    public Sprite sprite;
    public string Name;
    public Hardware(float computationPower, int upgradePrice, Sprite sprite, string name)
    {
        this.computationPower = computationPower;
        this.upgradePrice = upgradePrice;
        this.sprite = sprite;
        this.Name = name;
    }
}
