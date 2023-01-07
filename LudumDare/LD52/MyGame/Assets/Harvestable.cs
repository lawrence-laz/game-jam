using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public GameObject DropPrefab;

    public void Harvest()
    {
        var drop = Instantiate(DropPrefab);
        drop.transform.position = transform.position;
        Destroy(gameObject);
    }
}
