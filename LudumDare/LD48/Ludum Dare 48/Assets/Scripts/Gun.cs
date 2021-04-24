using UnityEngine;

public class Gun : MonoBehaviour
{
    public float FireRate; // Bullets per second.
    public float BulletSpeed;
    public GameObject BulletPrefab;

    public bool IsShooting;

    private float _nextBulletShootAt;
    private Transform _nozzle;
    private Lander _lander;

    private void Start()
    {
        _nozzle = transform.Find("nozzle");
        _lander = transform.parent.GetComponentInChildren<Lander>();
    }

    private void Update()
    {
        if (_lander.IsLanded && _lander.Target != null && _lander.Target.GetComponentInParent<SpaceStation>() != null)
        {
            return;
        }

        ShootBullet();
    }

    private void ShootBullet()
    {
        if (!IsShooting || Time.time < _nextBulletShootAt)
        {
            return;
        }

        _nextBulletShootAt = Time.time + 1f / FireRate;
        var bullet = Instantiate(BulletPrefab, _nozzle.position, transform.rotation);
        bullet.transform.up = transform.up;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * BulletSpeed;
        bullet.GetComponent<Bullet>().ShotBy = gameObject;
    }
}
