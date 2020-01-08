using DG.Tweening;
using UnityEngine;

public class CloudSystem : MonoBehaviour
{
    public GameObject Clouds;

    private void Start()
    {
        Instantiate(Clouds);

        foreach (var cloud in GameObject.FindGameObjectsWithTag("Cloud"))
        {
            SetCloudFlying(cloud.transform);
        }
    }

    private void Update()
    {
        foreach (var cloud in GameObject.FindGameObjectsWithTag("Cloud"))
        {
            SetSorting(cloud.GetComponent<SpriteRenderer>());
        }
    }

    private void SetCloudFlying(Transform transform)
    {
        float addition = (transform.position.y > 0 ? -15 : 15);
        float duration = Random.Range(20, 40);

        DOTween.Sequence()
            .Append(DOTween.Sequence()
                .Append(transform.DOMoveX(transform.position.x + addition, duration))
                .Append(transform.DOMoveX(transform.position.x - addition, duration))
            )
            .Join(DOTween.Sequence()
                .Append(transform.DOScaleX(Mathf.Sign(transform.localScale.x) * Random.Range(0.6f, 0.8f), duration / 12))
                .Append(transform.DOScaleX(Mathf.Sign(transform.localScale.x) * 1f, duration / 12))
                .Append(transform.DOScaleX(Mathf.Sign(transform.localScale.x) * Random.Range(0.6f, 0.8f), duration / 12))
                .Append(transform.DOScaleX(Mathf.Sign(transform.localScale.x) * 1f, duration / 12))
                .Append(transform.DOScaleX(Mathf.Sign(transform.localScale.x) * Random.Range(0.6f, 0.8f), duration / 12))
                .Append(transform.DOScaleX(Mathf.Sign(transform.localScale.x) * 1f, duration / 12)))
            .SetLoops(-1);
    }

    private void SetSorting(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sortingOrder = (spriteRenderer.transform.position.y > -3.3f ? -1000 : 1000);
    }
}
