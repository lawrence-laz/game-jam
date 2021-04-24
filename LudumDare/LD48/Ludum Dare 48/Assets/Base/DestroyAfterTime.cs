using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Duration;

    [Header("Avoid Destroying")]
    public bool CloseToPlayer = false;
    public float Distance = 3;

    private Transform _player;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterDuration());
        _player = GameObject.FindWithTag("Player").transform;
    }

    private IEnumerator DestroyAfterDuration()
    {
        do yield return new WaitForSeconds(Duration);
        while (CloseToPlayer && Distance >= _player.DistanceTo(transform));

        DOTween.Sequence()
            .Append(transform.DOScale(0, 1))
            .AppendCallback(() => Destroy(gameObject));
    }
}
