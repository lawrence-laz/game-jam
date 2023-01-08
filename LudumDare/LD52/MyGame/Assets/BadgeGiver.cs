using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeGiver : MonoBehaviour
{
    public Badges BadgeName;

    private void Start()
    {
        if (Badge.Set(BadgeName))
        {
            FindObjectOfType<AchievementEffectSpawner>().SpawnEffect(transform.position);
        }
    }
}
