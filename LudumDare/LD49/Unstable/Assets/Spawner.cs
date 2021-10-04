using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs;
    public float SpawnHeight;
    public float SpawnAreaWidth;

    public float LastSpawnedAt;
    public float SpawnPeriod = 2;
    public bool DirectToFacePlayer = true;

    private void Update()
    {
        if (Time.time > LastSpawnedAt + SpawnPeriod)
        {
            LastSpawnedAt = Time.time;
            var spawned = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], null, true);
            if (DirectToFacePlayer)
                spawned.transform.forward = -transform.forward;
            var position = transform.position + transform.right * Random.Range(-SpawnAreaWidth, SpawnAreaWidth);
            position.y = SpawnHeight;
            spawned.transform.position = position;
        }
    }
}
