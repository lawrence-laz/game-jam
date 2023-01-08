using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    public string LabelTrigger;
    public int MaxCount = 1;
    public string SpawnedLabel;
    public GameObject Prefab;

    private void Update() 
    {
        var spawnedCount = FindObjectsOfType<Label>().Count(label => label.Is(SpawnedLabel));
        if (spawnedCount >= MaxCount)
        {
            return;
        }

        var isTrigerred = FindObjectsOfType<Label>().Any(label => label.Is(LabelTrigger));
        if (isTrigerred)
        {
            var obj = Instantiate(Prefab);
            obj.transform.position = Vector3.left * 10;
        }
    }
}
