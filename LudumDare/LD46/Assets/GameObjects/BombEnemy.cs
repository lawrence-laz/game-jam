using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public Death Death { get; set; }

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        Death = GetComponent<Death>();
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        Death.Die();
    }
}
