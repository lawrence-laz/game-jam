using DG.Tweening;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class ArrowDamage : MonoBehaviour
{
    public UnityEvent OnHit;
    public int Damage = 1;
    public bool Persistant;

    private HeroStats _stats;
    private Rigidbody2D _body;
    private ArrowFly _fly;
    private int _bounces = 3;
    private Vector2 _lastPosition;
    private int _didNotMove = 0;
    private float _startTime;

    private void OnEnable()
    {
        _stats = HeroStats.Get();
        _body = GetComponent<Rigidbody2D>();
        _fly = GetComponent<ArrowFly>();
        _lastPosition = Vector2.one * 123;
        _startTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name .StartsWith("Wall")
            && Time.time - _startTime < 0.05f)
        {
            return;
        }

        DamageEnemy(collision);
        FlashEnemy(collision);
        if (!Persistant)
        {
            if (_stats.BounceArrows && _bounces > 0)
            {
                RepeatedAudio.Get("EasyHit1_Audio").PlayTimes++;
                _bounces--;
                transform.up = transform.up - 2 *
                    Vector3.Project(transform.up, collision.contacts[0].normal);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_didNotMove > 10 && !Persistant)
        {
            Destroy(gameObject);
        }

        if ((Vector2)transform.position == _lastPosition)
        {
            _didNotMove++;
        }
        else
        {
            _didNotMove = 0;
        }

        _lastPosition = transform.position;
    }

    private static void FlashEnemy(Collision2D collision)
    {
        var spriteFlashes = collision.collider.transform.root.GetComponentsInChildren<SpriteFlash>();
        var stutter = false;
        foreach (var flash in spriteFlashes)
        {
            flash.WhiteSprite();
            DOTween.Sequence()
                .AppendInterval(.1f)
                .AppendCallback(() =>
                {
                    if (!stutter)
                    {
                        //Thread.Sleep(40);
                        RepeatedStutter.DoIt();
                        stutter = true;
                    }
                    if (flash != null)
                    {
                        flash.NormalSprite();
                    }
                });
        }
    }

    private void DamageEnemy(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            ScreenShake.Get().ShortShake();
            Debug.Log("<color=red>Hit enemy</color>");
            OnHit.Invoke();
            enemy.Health -= Damage;
        }
    }
}
