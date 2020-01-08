using UnityEngine;
using UnityEngine.AI;

public class MarchingBehaviour : MonoBehaviour
{
    public Transform PathParent;
    public Vector3 Target;
    public float WaitBetweenPoints;

    private int _currentTargetIndex = -1;
    private NavMeshAgent _nav;
    private NoticeBehaviour _notice;
    private bool _reachedDestination = false;
    private float _reachedDestinationTime = 0;

    public void CalculateNextTarget()
    {
        if (Time.time < _reachedDestinationTime + WaitBetweenPoints)
        {
            return;
        }

        _reachedDestination = false;
        _currentTargetIndex = (_currentTargetIndex + 1) % PathParent.childCount;
        Target = PathParent.GetChild(_currentTargetIndex).position;
        _nav.SetDestination(Target);
    }

    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _notice = GetComponent<NoticeBehaviour>();
        CalculateNextTarget();
    }

    private void Update()
    {
        if (_notice.HasNoticed
            || !_nav.enabled
            || _notice.SecondaryAttraction)
        {
            return;
        }

        if (HasReachedDestination())
        {
            CalculateNextTarget();
        }
    }

    private bool HasReachedDestination()
    {
        if (!_nav.pathPending)
        {
            if (_nav.remainingDistance <= _nav.stoppingDistance)
            {
                if (!_nav.hasPath || _nav.velocity.sqrMagnitude == 0f)
                {
                    if (!_reachedDestination)
                    {
                        _reachedDestination = true;
                        _reachedDestinationTime = Time.time;
                    }

                    return true;
                }
            }
        }

        return false;
    }
}
