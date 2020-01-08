using UnityEngine;

public class SpawnerSpawn : MonoBehaviour
{
    public GameObject[] SpawnPrefabs;
    public float Period;
    public int SpawnLimit = -1;
    public int MonstersOnScreenLimit = -1;

    private float _lastTimeSpawned;
    EntranceUnlock _door;

    private void OnEnable()
    {
        _lastTimeSpawned = Period / 2;
        _door = FindObjectOfType<EntranceUnlock>();
    }

    private void Update()
    {

        if (Time.time - _lastTimeSpawned < (Mathf.Min(2, Period + (1f - SpawnLimit * 0.1f))))
            return;

        _lastTimeSpawned = Time.time;
        if (MonstersOnScreenLimit != -1 && FindObjectsOfType<Monster>().Length >= MonstersOnScreenLimit)
            return;

        var prefab = SpawnPrefabs[Random.Range(0, SpawnPrefabs.Length)];
        Instantiate(prefab, transform.position, Quaternion.identity);

        SpawnLimit--;
        if (SpawnLimit == 0)
            MonstersOnScreenLimit = 0;
    }
}
