using UnityEngine;
using UnityEngine.Events;

public class ArroundProjectileShooter : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public UnityEvent OnShooting;
    public SimpleAnimator[] Animators;

    private EnemyStats _stats;
    private float _lastTimeShot;

    private void OnEnable()
    {
        _stats = GetComponent<EnemyStats>();
        _lastTimeShot = _stats.ProjectilesAuthoShootPeriod / 2;

    }

    private void Update()
    {
        PeriodicCircularShootLogic();
    }

    private void PeriodicCircularShootLogic()
    {
        if (_stats.ProjectilesAuthoShootPeriod == 0)
            return;

        if (Time.time - _lastTimeShot < _stats.ProjectilesAuthoShootPeriod)
            return;

        _lastTimeShot = Time.time;

        for (int i = 0; i < Animators.Length; i++)
        {
            var animator = Animators[i];
            if (i == 0)
            {
                animator.UpAndDownOnce(1, 0.2f, _ => ShootCircularBullets());
            }
            else
            {
                animator.UpAndDownOnce(1, 0.2f);
            }
        }
    }

    public void ShootCircularBullets()
    {
        OnShooting.Invoke();

        var bullets = _stats.ProjectilesCount;
        var stepSize = Mathf.PI * 2 / bullets;

        for (float i = 0; i < bullets; i += stepSize)
        {
            var direction = new Vector2(Mathf.Cos(i), Mathf.Sin(i));
            var bullet = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            bullet.transform.LookAt2D((Vector2)transform.position + direction);
            bullet.GetComponent<ArrowFly>().Speed = _stats.ProjectilesSpeed;
        }
    }
}
