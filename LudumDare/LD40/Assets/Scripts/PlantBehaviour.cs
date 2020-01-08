using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ReproductionBehaviour))]
public class PlantBehaviour : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float growthRate = 1;
    [SerializeField]
    private Sprite[] sprites;

    private ReproductionBehaviour reproduction;

    private void OnEnable()
    {
        reproduction = GetComponent<ReproductionBehaviour>();

        InvokeRepeating("ReproduceRandomly", 0.5f, 0.5f + Random.Range(0f, 0.15f));
    }

    private void Start()
    {
        RandomizeVisual();

        if (Time.timeSinceLevelLoad > 1)
            StartCoroutine(GrowAnimation());
    }

    private void RandomizeVisual()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        if (Random.value > 0.5f)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void ReproduceRandomly()
    {
        if (Random.Range(0.0f, 1.0f) > 1.00001f - growthRate)
            reproduction.Reproduce();
    }

    private IEnumerator GrowAnimation()
    {
        transform.localScale = Vector3.zero;
        float growSpeed = Random.Range(0.01f, 0.05f);

        yield return new WaitForEndOfFrame();

        do
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, growSpeed);
            yield return new WaitForEndOfFrame();
        } while (transform.localScale != Vector3.one);
    }
}
