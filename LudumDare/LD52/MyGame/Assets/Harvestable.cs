using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public GameObject DropPrefab;
    public ChanceDrop[] MorePrefabs;

    public void Harvest()
    {
        Spawn(DropPrefab);
        foreach(var prefab in MorePrefabs)
        {
            if (prefab.Chance >= Random.Range(0f, 1f))
            {
                Spawn(prefab.Prefab);
            }
        }
        Destroy(gameObject);
    }

    private void Spawn(GameObject prefab)
    {
        var drop = Instantiate(prefab);
        drop.transform.position = transform.position;
    }
}
