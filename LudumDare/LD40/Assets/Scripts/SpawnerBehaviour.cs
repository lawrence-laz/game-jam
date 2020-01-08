using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public delegate void Spawner(Vector2 position);

    [Header("Plants")]
    [SerializeField]
    private float plantThreshold;
    [SerializeField]
    private Vector2 stepSize;
    [SerializeField]
    private Transform from;
    [SerializeField]
    private Transform to;
    [SerializeField]
    private float scale;

    [Header("Carnivores")]
    [SerializeField]
    private float carnivoreThreshold;
    [SerializeField]
    private Vector2 initialCarnivoreAmount;

    [Header("Herbivores")]
    [SerializeField]
    private float herbivoreThreshold;
    [SerializeField]
    private Vector2 initialHerbivoresAmount;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject plant;
    [SerializeField]
    private GameObject carnivore;
    [SerializeField]
    private GameObject herbivore;

    private Transform plantContainer;
    private Transform carnivoreContainer;
    private Transform herbivoreContainer;

    public void SpawnSomePlants(int amount)
    {
        List<Vector2> positions = GetRandomPositionsByOpenSimplexNoise(plantThreshold, from.position, to.position, scale);
        for (int i = 0; i < amount; i++)
        {
            if (positions.Count == 0)
            {
                Debug.LogWarning("Not enough positions to spawn some plants.");
                return;
            }

            int randomIndex = Random.Range(0, positions.Count);
            SpawnPlant(positions[randomIndex]);
            positions.RemoveAt(randomIndex);
        }
    }

    public List<Vector2> GetRandomPositionsByOpenSimplexNoise(double threshold, Vector2 areaFrom, Vector2 areaTo, float scale, bool usePerlin = false)
    {
        OpenSimplexNoise noise = new OpenSimplexNoise();
        Vector2 randomOffset = Random.insideUnitCircle * Random.Range(100f, 10000f);
        List<Vector2> positions = new List<Vector2>();

        for (double x = areaFrom.x; x < areaTo.x; x += Random.Range(stepSize.x, stepSize.y))
        {
            for (double y = areaFrom.y; y < areaTo.y; y += Random.Range(stepSize.x, stepSize.y))
            {
                double randomValue = usePerlin ? Mathf.PerlinNoise((float)x * scale + randomOffset.x, (float)y * scale + randomOffset.y) : noise.Evaluate(x * scale, y * scale);
                if (randomValue < threshold)
                    continue;

                positions.Add(new Vector2((float)x, (float)y));
            }
        }

        return positions;
    }

    private void OnEnable()
    {
        plantContainer = GameObject.FindGameObjectWithTag("PlantContainer").transform;
        carnivoreContainer = GameObject.FindGameObjectWithTag("CarnivoreContainer").transform;
        herbivoreContainer = GameObject.FindGameObjectWithTag("HerbivoreContainer").transform;
    }

    private void Start()
    {
        SpawnInitialPlants();
        SpawnInitialHerbivores();
        SpawnInitialCarnivores();
    }

    private void SpawnInitialPlants()
    {
        List<Vector2> positions = GetRandomPositionsByOpenSimplexNoise(plantThreshold, from.position, to.position, scale);
        foreach (Vector2 position in positions)
        {
            SpawnPlant(position);
        }
    }

    private void SpawnInitialHerbivores()
    {
        List<Vector2> positions = GetRandomPositionsByOpenSimplexNoise(herbivoreThreshold, from.position, to.position, scale, true);
        int amount = (int)Random.Range(initialHerbivoresAmount.x, initialHerbivoresAmount.y);
        for (int i = 0; i < amount; i++)
        {
            if (positions.Count == 0)
            {
                Debug.LogWarning("Not enough positions for herbivores to spawn.");
                return;
            }

            int randomIndex = Random.Range(0, positions.Count);
            SpawnHerbivore(positions[randomIndex]);
            positions.RemoveAt(randomIndex);
        }
    }

    private void SpawnInitialCarnivores()
    {
        List<Vector2> positions = GetRandomPositionsByOpenSimplexNoise(carnivoreThreshold, from.position, to.position, scale, true);
        int amount = (int)Random.Range(initialCarnivoreAmount.x, initialCarnivoreAmount.y);
        for (int i = 0; i < amount; i++)
        {
            if (positions.Count == 0)
            {
                Debug.LogWarning("Not enough positions for carnivores to spawn.");
                return;
            }

            int randomIndex = Random.Range(0, positions.Count);
            SpawnCarnivore(positions[randomIndex]);
            positions.RemoveAt(randomIndex);
        }
    }

    private void SpawnPlant(Vector2 position)
    {
        Instantiate(plant, position, transform.rotation, plantContainer);
    }

    private void SpawnCarnivore(Vector2 position)
    {
        Instantiate(carnivore, position, transform.rotation, carnivoreContainer);
    }

    private void SpawnHerbivore(Vector2 position)
    {
        Instantiate(herbivore, position, transform.rotation, herbivoreContainer);
    }
}
