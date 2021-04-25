using UnityEngine;
using UnityEngine.Events;

public class Lander : MonoBehaviour
{
    public float AcceptableLandingSpeed = 1;
    public float AcceptableLandingAngle = 20;
    public UnityEventGameObject OnLand;
    public UnityEvent OnLiftOff;
    public bool IsLanded;
    public GameObject Target;

    private Rigidbody2D _body;
    private FixedJoint2D _joint;
    private Health _health;

    public void Detach()
    {
        _joint.connectedBody = null;
        _joint.enabled = false;
        Debug.Log("Detached");
        if (Target != null)
        {
            Target.SendMessageUpwards("OnDetach", transform.parent.gameObject, SendMessageOptions.DontRequireReceiver);
        }
        Target = null;
    }

    private void Start()
    {
        _body = GetComponentInParent<Rigidbody2D>();
        _joint = GetComponentInParent<FixedJoint2D>();
        _health = GetComponentInParent<Health>();
        _health.OnDead.AddListener(OnDead);
    }

    private void OnDead()
    {
        enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsLanded)
        {
            return;
        }
        Debug.Log("Landing trigerred");

        if (collision.GetComponent<DestroyAfterTime>() != null)
        {
            // Don't land on temporary props.
            return;
        }

        if (!IsLandingAngleCorrect(collision.transform))
        {
            return;
        }

        Debug.Log($"{_body.velocity.magnitude} <= {AcceptableLandingSpeed}");

        if (_body.velocity.magnitude <= AcceptableLandingSpeed)
        {
            Attach(collision);
        }
    }

    private void Attach(Collider2D target)
    {
        if (_health.CurrentValue <= 0)
        {
            return;
        }

        Target = target.gameObject;
        IsLanded = true;
        _body.velocity = Vector2.zero;
        _body.angularVelocity = 0;
        OnLand.Invoke(target.gameObject);
        _joint.connectedBody = target.attachedRigidbody;
        _joint.enabled = true;
        target.SendMessageUpwards("OnAttach", transform.parent.gameObject, SendMessageOptions.DontRequireReceiver);
        // _joint.connectedAnchor ?
        Debug.Log("Attached");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsLanded)
        {
            IsLanded = false;
            Detach();
            OnLiftOff.Invoke();
        }
    }

    private bool IsLandingAngleCorrect(Transform landingTarget)
    {
        var ideal = (transform.position - landingTarget.position).normalized;
        var current = transform.up;

        if (Vector2.Angle(ideal, current) <= AcceptableLandingAngle)
        {
            return true;
        }
        {
            Debug.Log($"Incorrect landing angle between {current} and {ideal}: {Vector2.Angle(ideal, current)} <= {AcceptableLandingAngle}");
            return false;
        }
    }
}
