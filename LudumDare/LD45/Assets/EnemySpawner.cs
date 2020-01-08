using Libs.Base.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float SpawnFrequency = 5;
    public Vector2 SpawnRadius;
    public int count;
    public bool AvoidVertical;

    private List<GameObject> spawned = new List<GameObject>();
    private float lastSpawnAt;

    public Score Score { get; private set; }

    private void Start()
    {
        Score = FindObjectOfType<Score>();

        if (count != 0)
            InvokeRepeating("SpawnByCount", 0, 3);
    }

    private void SpawnByCount()
    {
        if (count != 0)
        {
            spawned = spawned.Where(x => x != null).ToList();
            var toSpawn = Mathf.Max(0, count - spawned.Count);
            for (int i = 0; i < toSpawn; i++)
            {
                spawned.Add(Spawn());
            }
        }
    }

    private void Update()
    {
        SpawnFrequency = Score.Current < 2 ? 8.5f
            : Score.Current < 4 ? 6.5f
            : Score.Current < 8 ? 4.5f
            : Score.Current < 16 ? 3.5f
            : Score.Current < 20 ? 3
            : 2f;

        //Debug.Log("spawn per" + SpawnFrequency);

        if (count == 0 && Time.time >= lastSpawnAt + SpawnFrequency)
        {
            lastSpawnAt = Time.time;
            Spawn();
        }
    }

    private GameObject Spawn()
    {
        //Debug.Log($"Spawned, total {spawned.Count}");

        var spawnPosition = Random.onUnitSphere;
        if (AvoidVertical)
            spawnPosition.y /= 3;
        spawnPosition = spawnPosition.normalized;
        spawnPosition.x *= Random.Range(SpawnRadius.x, SpawnRadius.y);
        spawnPosition.z *= Random.Range(SpawnRadius.x, SpawnRadius.y);
        if (!AvoidVertical)
            spawnPosition.y *= Random.Range(SpawnRadius.x, SpawnRadius.y) / 3;
        else
            spawnPosition.y = Mathf.Abs(transform.position.y - spawnPosition.y) > 10
                ? Mathf.Sign(spawnPosition.y) * 10
                : spawnPosition.y;

        return Instantiate(Enemies.GetRandom(), transform.position + spawnPosition, Quaternion.identity);
    }
}
