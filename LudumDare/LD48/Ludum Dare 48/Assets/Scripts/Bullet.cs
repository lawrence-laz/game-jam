using Libs.Base.Effects;
using Libs.Base.Extensions;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ShotBy;
    public float Damage = 2;
    public GameObject ExplosionFx;
    public GameObject SimpleHitFx;
    public AudioClip HitSound;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponentInChildren<AudioSource>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && ShotBy != null && ShotBy.DoesShareHierarchy(collision.gameObject))
        {
            return;
        }

        if (collision.TryGetComponent<Health>(out var health)
            || collision.gameObject.TryGetComponentInParent(out health))
        {
            health.CurrentValue -= Damage;
            if (health.CurrentValue <= 0)
            {
                Explode(health.transform.position);
                if (!health.gameObject.CompareTag("Player"))
                {
                    Destroy(health.gameObject);
                }
            }
        }
        else if (collision.TryGetComponent<Asteroid>(out var asteroid)
            || collision.gameObject.TryGetComponentInParent(out asteroid))
        {
            asteroid.CurrentValue -= Damage;
            if (asteroid.CurrentValue <= 0)
            {
                Explode(asteroid.transform.position);
                asteroid.BigExposion = true;
            }
        }
        else
        {
            ScreenShake.Get().ShortShake(); ;
        }

        if (collision.TryGetComponent<SpriteFlash>(out var flash))
        {
            flash.Blink();
        }

        Thread.Sleep(10);
        Destroy(gameObject, 0.5f);
        HitEffect(transform.position);
    }
    private void Explode(Vector3 position)
    {
        ScreenShake.Get().MediumShake();
        Instantiate(ExplosionFx, position, Quaternion.identity);
    }

    private void HitEffect(Vector3 position)
    {
        if (_audio != null)
        {
            _audio.PlayOneShot(HitSound);
            _audio.transform.parent = null;
        }

        if (SimpleHitFx == null)
        {
            return;
        }

        Instantiate(SimpleHitFx, position, Quaternion.identity);
    }
}
