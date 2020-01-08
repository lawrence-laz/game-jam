using System.Collections;
using UnityEngine;

public class ReproductionBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private string prefabLocation;
    [SerializeField]
    private float spawnRadius = 5;
    [SerializeField]
    private GameObject effectPrefab;
    [SerializeField]
    private int maxCrowd;
    [SerializeField]
    private bool log = false;

    private GameObject effectObject;
    private float lastTimeReproduced;
    private Transform center;

    public float LastTimeReproduced { get { return lastTimeReproduced; } }

    private void OnEnable()
    {
        prefab = Resources.Load<GameObject>(prefabLocation);
        center = GameObject.Find("Bounds/Center").transform;
    }

    public void Reproduce()
    {
        Vector2 newPosition = transform.position;
        newPosition.x += Random.Range(-spawnRadius, spawnRadius);
        newPosition.y += Random.Range(-spawnRadius, spawnRadius);
        bool movedAwayFromSides = false;
        int sameSpeciesAround = 0;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(newPosition, spawnRadius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (!movedAwayFromSides && hit.transform != null && hit.transform.tag == "Bounds")
            {
                movedAwayFromSides = true;
                newPosition += ((Vector2)center.position - newPosition).normalized * spawnRadius * 2;
            }
            if (hit.transform.tag == tag)
            {
                sameSpeciesAround++;
            }
        }

        if (sameSpeciesAround <= maxCrowd)
            Instantiate(prefab, newPosition, transform.rotation, transform.parent);
        else if (log)
            Debug.Log("Too crowded for " + gameObject.name);

        lastTimeReproduced = Time.time;

        if (gameObject.activeSelf)
            StartCoroutine(DoEffect());
    }

    private IEnumerator DoEffect()
    {
        if (effectObject != null || effectPrefab == null)
            yield break;

        effectObject = Instantiate(effectPrefab, transform.position, transform.rotation, transform);
        yield return new WaitForSeconds(1 + Random.Range(0f, 1f));
        Destroy(effectObject);
    }
}
