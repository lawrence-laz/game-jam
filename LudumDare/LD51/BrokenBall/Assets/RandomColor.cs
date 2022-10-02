using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Color ColorRangeStart;
    public Color ColorRangeEnd;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(ColorRangeStart, ColorRangeEnd, Random.Range(0f, 1f));
    }
}
