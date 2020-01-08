using UnityEngine;
using UnityEngine.AI;
using UnityGoodies;

public class SwordGuyBehaviour : MonoBehaviour
{
    public float DistanceToHit = 2;

    private Transform _player;
    private NoticeBehaviour _notice;

    public void HandleDeath()
    {
        Debug.LogFormat("{0} has died.", gameObject.name);
        gameObject.EnableComponentsInChildren(false,
            typeof(NavMeshAgent),
            typeof(FollowBehaviour),
            typeof(SwordGuyBehaviour),
            typeof(MarchingBehaviour),
            typeof(FaceTowardsTargetBehaviour)
        );

        var rightHand = transform.Find("RightHand");
        rightHand.SetParent(null);
        rightHand.gameObject.AddComponent<Rigidbody>();
        rightHand.gameObject.EnableCollidersInChildren(true);
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _notice = GetComponent<NoticeBehaviour>();
    }

    private void Update()
    {
        //Debug.LogFormat("{0} destination is {1}", gameObject.name, GetComponent<NavMeshAgent>().destination);

        if (_player.position.DistanceTo(transform.position) <= DistanceToHit
            && _notice.HasNoticed && _notice.IsInLineOfSight)
        {
            TryAttackPlayer();
        }
    }

    private void TryAttackPlayer()
    {
        var additionalOffset = transform.position.DirectionTo(Camera.main.transform.position);
        additionalOffset.y = 0;

        if (this.ForAllComponentsInRootsChildren<SwordBehaviour>(
            sword => sword.Attack(Camera.main.transform.position + additionalOffset * 0.1f + Vector3.left * 0.3f, _player)
        ))
        {
            enabled = false;
        }
    }
}
