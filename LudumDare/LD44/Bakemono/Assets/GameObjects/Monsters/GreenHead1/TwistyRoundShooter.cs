using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TwistyRoundShooter : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject FollowProjectilePrefab;
    public UnityEvent OnShooting;
    public SimpleAnimator[] Animators;

    private EnemyStats _stats;
    Transform _player;
    float _startHealth;

    private void OnEnable()
    {
        _stats = GetComponent<EnemyStats>();
        _player = HeroStats.Get().transform;
        _startHealth = _stats.Health;
    }

    private void Start()
    {
        StartCoroutine(PeriodicCircularShootLogic());
    }

    private IEnumerator PeriodicCircularShootLogic()
    {
        while (true)
        {
            yield return new WaitForSeconds(_stats.ProjectilesAuthoShootPeriod / 10f);

            for (int i = 0; i < Animators.Length; i++)
            {
                var animator = Animators[i];
                if (i == 0)
                {
                    try
                    {
                        animator.UpAndDownOnce(1, 0.2f, _ => StartCoroutine(ShootFollowBullets()));
                    }
                    catch (Exception)
                    {
                        Debug.LogError("hmmmmmmmmmm");
                    }
                }
                else
                {
                    animator.UpAndDownOnce(1, 0.2f);
                }
            }

            yield return new WaitForSeconds(_stats.ProjectilesAuthoShootPeriod / 10f * 9f);
        }
    }

    public IEnumerator ShootFollowBullets()
    {
        OnShooting.Invoke();

        var bullets = Mathf.Max(10, _stats.ProjectilesCount * ((_startHealth - _stats.Health) / _startHealth));
        var circleTarget = Mathf.PI * 6f;
        var stepSize = circleTarget / bullets; // 4 for two circles

        for (float i = 0; i < circleTarget; i += stepSize)
        {
            var direction = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
            var directionToPlayer = transform.DirectionTo(_player);
            var projectionToPlayer = Vector3.Dot(direction, directionToPlayer);
            if (projectionToPlayer < 0)
            {
                continue;
            }

            var bullet = Instantiate(FollowProjectilePrefab, transform.position + (Vector3)direction * 1f, Quaternion.identity);
            bullet.transform.up = direction;
            bullet.GetComponent<ArrowFly>().Speed = _stats.ProjectilesSpeed;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator ShootCircularBullets()
    {
        OnShooting.Invoke();

        var bullets = _stats.ProjectilesCount;
        var stepSize = Mathf.PI * 4.5f / bullets; // 4 for two circles

        for (float i = 0; i < bullets; i += stepSize)
        {
            var direction = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
            var bullet = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            bullet.transform.LookAt2D((Vector2)transform.position + direction);
            bullet.GetComponent<ArrowFly>().Speed = _stats.ProjectilesSpeed;
            yield return new WaitForSeconds(0.071f);
        }
    }
}
