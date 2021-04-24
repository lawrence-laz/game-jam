using DG.Tweening;
using UnityEngine;

public class SpaceshipExplosion : MonoBehaviour
{
    public GameObject CrewEvacuationFx;
    public GameObject ExplosionFx;

    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDead.AddListener(OnDead);
    }

    private void OnDead()
    {
        Transform crew = null;
        DOTween.Sequence()
            .AppendCallback(() => crew = Instantiate(CrewEvacuationFx, transform).transform)
            .AppendInterval(5) // Seconds.
            .AppendCallback(() =>
            {
                crew.SetParent(null);
                Instantiate(ExplosionFx, transform.position, Quaternion.identity);
                Destroy(gameObject);
            })
            .Play();
    }
}
