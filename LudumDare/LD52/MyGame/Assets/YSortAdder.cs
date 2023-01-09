using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSortAdder : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating(nameof(AddMissingYSorters), 0.1f, 0.1f);
    }

    public void AddMissingYSorters()
    {
        var renderers = FindObjectsOfType<SpriteRenderer>();
        foreach (var renderer in renderers)
        {
            if (renderer.GetComponent<YSorter>() == null)
            {
                renderer.gameObject.AddComponent<YSorter>();
            }
        }
    }
}
