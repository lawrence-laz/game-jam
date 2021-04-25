using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Duration;
    public bool ScaleOut = true;

    [Header("Avoid Destroying")]
    public bool CloseToPlayer = false;
    public float Distance = 3;

    private Transform _player;

    public void StartManually(float duration, float distance)
    {
        CloseToPlayer = true;
        Duration = duration;
        Distance = distance;
        StartCoroutine(DestroyAfterDuration());
    }

    private void OnEnable()
    {
        if (Duration != 0 || CloseToPlayer)
        {
            StartCoroutine(DestroyAfterDuration());
        }
        var playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            _player = playerObject.transform;
        }
    }

    private IEnumerator DestroyAfterDuration()
    {
        do yield return new WaitForSeconds(Duration);
        while (CloseToPlayer && _player != null && Distance >= _player.DistanceTo(transform));

        if (ScaleOut)
        {
            DOTween.Sequence()
                .Append(transform.DOScale(0, 1))
                .AppendCallback(() => Destroy(gameObject));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
