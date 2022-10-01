using DG.Tweening;
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

    [Header("Internals")]
    public float LastSpawnDistance = 0;
    private Sequence _sequence;

    void Start()
    {
        FindObjectOfType<Timer>().OnTenSeconds.AddListener(EveryTenSeconds);
    }

    private void Update()
    {
        if (IsStarted)
        {
            transform.position += (Vector3)StepVector / 10 * Time.deltaTime;
            var currentDistance = transform.position.y;
            if (Mathf.Abs(currentDistance - LastSpawnDistance) >= SpawnPeriodByDistance)
            {
                Instantiate(SimpleTileLine, SpawnPoint.position, Quaternion.identity, transform);
                LastSpawnDistance = currentDistance;
            }
        }
    }

    private void EveryTenSeconds()
    {
        IsStarted = true;
        // Debug.Log($"Current time: {Time.time}");
        // Instantiate(SimpleTileLine, SpawnPoint.position, Quaternion.identity, transform);
        // transform.DOLocalMove((Vector3)StepVector, 3).SetRelative(true);
    }
}
