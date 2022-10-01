using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float StartedAt = 0;
    public Vector2 StepVector;
    public Transform SpawnPoint;

    [Header("Spawner prefabs")]
    public GameObject SimpleTileLine;

    void Start()
    {
        FindObjectOfType<Timer>().OnTenSeconds.AddListener(EveryTenSeconds);
    }

    private void EveryTenSeconds()
    {
        Instantiate(SimpleTileLine, SpawnPoint.position, Quaternion.identity, transform);
        transform.localPosition += (Vector3)StepVector;
    }
}
