using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAiSystem : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        foreach (var enemy in FindObjectsOfType<SimpleEnemyComponent>())
        {
            EmulateAi(enemy);
        }
    }

    private void EmulateAi(SimpleEnemyComponent enemy)
    {
        WeaponComponent weapon = enemy.GetComponentInChildren<WeaponComponent>();
        Vector2 vectorToPlayer = _player.position - enemy.transform.position;
        if (weapon.Range >= vectorToPlayer.magnitude)
        {

        }
    }
}
