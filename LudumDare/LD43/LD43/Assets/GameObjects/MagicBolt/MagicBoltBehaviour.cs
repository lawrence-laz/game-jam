using System;
using UnityEngine;
using UnityEngine.Events;
using UnityGoodies;


public class MagicBoltBehaviour : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed;
    public float Damage;
    public LayerMask Targets;
    public float ExplosionForce;
    public float AttractAttentionDistance = 4;

    private bool _hasExploded = false;
    private Rigidbody _body;

    [Header("Effects")]
    public ParticleSystem ExplosionParticles;
    public ParticleSystem MovementParticles;
    public UnityEvent OnImpact;

    public void SetTarget(Vector3 target)
    {
        Direction = transform.position.DirectionTo(target);
    }

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_hasExploded && !ExplosionParticles.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _body.position += Direction * Speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogFormat("{0} collided with {1}", gameObject.name, collision.gameObject.name);

        if (!collision.gameObject.IsInLayerFrom(Targets))
        {
            return;
        }
        Explode();
        OnImpact.Invoke();

        var collisions = Physics.SphereCastAll(transform.position, 1, Direction, 0.35f);
        foreach (var col in collisions)
        {
            Debug.LogFormat("Explosion affected {0}", col.transform.GetRoot().gameObject.name);

            if (!col.collider.gameObject.IsInLayerFrom(Targets))
            {
                continue;
            }

            col.collider.ForAllComponentsInRootsChildren<HealthBehaviour>(
                hp => hp.Health -= Damage
            );

            col.collider.ForAllComponentsInChildren<GateBehaviour>(
                gate =>
                {
                    gate.BreakOpen();
                    Debug.Log("Bolt open the door");
                }
            );

            if (col.rigidbody != null)
            {
                Debug.LogFormat("Adding explosion force to {0}", col.transform.gameObject.name);
                var directionToExplosion = col.transform.position.DirectionTo(transform.position);
                col.rigidbody.AddExplosionForce(ExplosionForce, col.transform.position + directionToExplosion * 0.5f, 3);
            }
        }

        Direction = Vector3.zero;
    }

    private void Explode()
    {
        ExplosionParticles.gameObject.SetActive(true);
        MovementParticles.gameObject.SetActive(false);
        _hasExploded = true;

        CallNearbyEnemies();
    }

    private void CallNearbyEnemies()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.transform.position.DistanceTo(transform.position) > AttractAttentionDistance)
            {
                continue;
            }

            enemy.transform.ForAllComponentsInChildren<NoticeBehaviour>(
                notice => notice.CheckSound(transform.position)
            );
        }
    }
}
