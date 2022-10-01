using DG.Tweening;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float StartedAt = 0;
    public Vector2 StepVector;
    public Transform SpawnPoint;

    private Sequence _sequence;

    [Header("Spawner prefabs")]
    public GameObject SimpleTileLine;

    void Start()
    {
        FindObjectOfType<Timer>().OnTenSeconds.AddListener(EveryTenSeconds);
    }

    private void EveryTenSeconds()
    {
        Debug.Log($"Current time: {Time.time}");
        Instantiate(SimpleTileLine, SpawnPoint.position, Quaternion.identity, transform);
        transform.DOLocalMove((Vector3)StepVector, 3).SetRelative(true);
    }
}
