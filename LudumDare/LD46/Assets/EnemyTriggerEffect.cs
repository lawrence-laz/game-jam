using DG.Tweening;
using UnityEngine;

public class EnemyTriggerEffect : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);

        Destroy(gameObject, 0.5f);
    }
}
