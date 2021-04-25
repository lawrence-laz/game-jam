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
    private Renderer _renderer;
    private Health _health;
    private AudioSource _audioSource;

    private void Start()
    {
        _nozzle = transform.Find("nozzle");
        _lander = transform.parent.GetComponentInChildren<Lander>();
        _renderer = GetComponent<Renderer>();
        _health = GetComponentInParent<Health>();
        _audioSource = GetComponent<AudioSource>();
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
        if (_health != null && _health.CurrentValue <= 0)
        {
            return;
        }

        if (!_renderer.isVisible)
        {
            return;
        }

        if (!IsShooting || Time.time < _nextBulletShootAt)
        {
            return;
        }

        _nextBulletShootAt = Time.time + 1f / FireRate;
        var bullet = Instantiate(BulletPrefab, _nozzle.position, transform.rotation);
        var parentLayer = transform.parent.gameObject.layer;

        bullet.layer = parentLayer == LayerMask.NameToLayer("Player")
            ? LayerMask.NameToLayer("PlayerBullets")
            : parentLayer == LayerMask.NameToLayer("Enemies")
            ? LayerMask.NameToLayer("EnemyBullets")
            : LayerMask.NameToLayer("Default");

        bullet.transform.up = transform.up;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * BulletSpeed;
        bullet.GetComponent<Bullet>().ShotBy = gameObject;
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
