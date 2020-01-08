using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "CoinData", order = 0)]
public class CoinData : ScriptableObject
{
    public Sprite IconSprite;
    public float StartingValue;
    public string coinName;
}
