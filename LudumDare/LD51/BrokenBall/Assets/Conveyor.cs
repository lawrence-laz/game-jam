using DG.Tweening;
using Libs.Base.Extensions;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float StartedAt = 0;
    public Vector2 StepVector;
    public Transform SpawnPoint;
    public bool IsStarted;
    public float SpawnPeriodByDistance;

    [Header("Spawner prefabs")]
    public GameObject SimpleTileLine;
    public GameObject[] UpgradePrefabs;

    [Header("Internals")]
    public float LastSpawnDistance = 0;
    public bool ShouldSpawnUpgrade;
    private Sequence _sequence;

    void Start()
    {
        FindObjectOfType<Timer>().OnTenSeconds.AddListener(EveryTenSeconds);
        FindObjectsOfType<UpgradeSpawner>().GetRandom().AddUpgrade(UpgradePrefabs.GetRandom());
    }

    private void Update()
    {
        if (IsStarted)
        {
            // transform.position += (Vector3)StepVector / 10 * Time.deltaTime;
            var currentDistance = transform.position.y;
            if (Mathf.Abs(currentDistance - LastSpawnDistance) >= SpawnPeriodByDistance)
            {
                var line = Instantiate(SimpleTileLine, SpawnPoint.position, Quaternion.identity, transform);
                if (ShouldSpawnUpgrade)
                {
                    var spawner = line.GetComponentsInChildren<UpgradeSpawner>().GetRandom();
                    spawner.AddUpgrade(UpgradePrefabs.GetRandom());
                    ShouldSpawnUpgrade = false;
                }
                LastSpawnDistance = currentDistance;
            }
        }
    }

    private void EveryTenSeconds()
    {
        IsStarted = true;
        ShouldSpawnUpgrade = true;
        if (IsStarted)
        {
            var speedMultiplier = FindObjectOfType<Highscore>().SpeedMultiplier;
            transform.DOMove(StepVector * speedMultiplier, 1).SetRelative(true);
        }
        // Debug.Log($"Current time: {Time.time}");
        // Instantiate(SimpleTileLine, SpawnPoint.position, Quaternion.identity, transform);
        // transform.DOLocalMove((Vector3)StepVector, 3).SetRelative(true);
    }
}
