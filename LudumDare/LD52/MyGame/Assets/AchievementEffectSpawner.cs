using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEffectSpawner : MonoBehaviour
{
    public GameObject EffectPrefab;

    public void SpawnEffect(Vector3 position)
    {
        Instantiate(EffectPrefab, position, Quaternion.identity);
    }
}
