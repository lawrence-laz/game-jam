using UnityEngine;
using UnityEngine.AI;

public class FollowBehaviour : MonoBehaviour
{
    public Transform Target;

    private NavMeshAgent _nav;
    private NoticeBehaviour _notice;

    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _notice = GetComponent<NoticeBehaviour>();
        _nav.SetDestination(transform.position);
        _nav.ResetPath();
        InvokeRepeating("LookForDestination", 1, .5f);
        if (Target == null)
        {
            Target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void LookForDestination()
    {
        if (!enabled)
            return;

        if (_nav.enabled)
        {
            _nav.ResetPath();

            if (_notice.JustInSight)
            {
                var stillInSight = _notice.IsInLineOfSight;
            }

            _nav.SetDestination(_notice.JustInSight ? (Target != null ? Target.position : transform.position) : _notice.LastSightPosition);
        }
    }
}
