using UnityEngine;
using UnityEngine.AI;
using UnityGoodies;

public class BowGuyBehaviour : MonoBehaviour
{
    public float ShootPeriod = 2.5f;
    public float Distance = 10;
    public GameObject DeadColliderPrefab;

    private BowBehaviour _bow;
    private float _lastShotTime = 0;
    private NoticeBehaviour _notice;
    private HealthBehaviour _playerHp;
    private NavMeshAgent _nav;

    public void HandleDeath()
    {
        Debug.LogFormat("{0} has died.", gameObject.name);
        gameObject.EnableComponentsInChildren(false,
            typeof(NavMeshAgent),
            typeof(FollowBehaviour),
            typeof(BowGuyBehaviour),
            typeof(FaceTowardsTargetBehaviour)
        );

        foreach (var arrow in GetComponentsInChildren<ArrowBehaviour>())
        {
            Destroy(arrow.gameObject);
        }

        gameObject.EnableComponents<AttentionAttractorBehaviour>();

        gameObject.EnableCollidersInChildren(false);
        var deadCollider = Instantiate(DeadColliderPrefab).transform;
        deadCollider.SetParent(transform);
        deadCollider.localPosition = Vector3.zero;
        deadCollider.localRotation = Quaternion.identity;
    }

    private void Start()
    {
        _notice = GetComponent<NoticeBehaviour>();
        _bow = GetComponentInChildren<BowBehaviour>();
        _playerHp = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBehaviour>();
        _nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_notice.IsInLineOfSight
            && _notice.HasNoticed
            && transform.position.DistanceTo(Camera.main.transform.position) <= Distance
            && _playerHp.IsAlive)
        {
            ShootBow();
        }
    }

    private void ShootBow()
    {
        if (Time.time < _lastShotTime + ShootPeriod)
        {
            return;
        }

        _lastShotTime = Time.time;
        _bow.ShootArrow(Camera.main.transform); // TODO: actual target should be given, for better aim
    }
}
