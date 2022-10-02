using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Libs.Base.Extensions;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public GameObject[] Spawns1;
    public GameObject[] Spawns2;
    public GameObject[] Spawns3;
    public GameObject[] Spawns4;
    public GameObject[] Spawns5;
    public GameObject[] Spawns6;
    public GameObject[] SpawnsEnd;

    [Header("Internals")]
    public int SpawnIndex = 0;

    private void Start()
    {
        FindObjectOfType<Timer>().OnTenSeconds.AddListener(OnTenSeconds);
    }

    private void OnTenSeconds()
    {
        var prefabBag = SpawnIndex switch
        {
            0 => Spawns1,
            1 => Spawns2,
            2 => Spawns3,
            3 => Spawns4,
            4 => Spawns5,
            5 => Spawns6,
            _ => SpawnsEnd,
        };

        var destroyDuration = 2;
        for (var i = 0; i < transform.childCount; ++i)
        {
            Debug.Log("AAAAAAAAAAAAAAAAA");
            transform.GetChild(i).DOMove(Vector3.down * 15, destroyDuration).SetEase(Ease.InExpo);
            Destroy(transform.GetChild(i).gameObject, destroyDuration);
        }

        Instantiate(prefabBag.GetRandom(), transform);
    }
}
