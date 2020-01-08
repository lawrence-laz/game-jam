using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private bool flee;

    private Rigidbody2D body;
    private Animator animator;

    public Transform TargetTransform { get { return targetTransform; } set { targetTransform = value; } }
    public bool Flee { get { return flee; } set { flee = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

    private void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (targetTransform == null
            || (targetTransform.position - transform.position).magnitude <= 0.07f
            || GetComponent<SleepingBehaviour>() != null)
        {
            if (animator != null)
                animator.SetBool("IsMoving", false);
            return;
        }

        if (animator != null)
            animator.SetBool("IsMoving", true);

        Vector3 targetDirection = (targetTransform.position - transform.position).normalized * (flee ? -1 : 1);

        Vector3 scale = transform.localScale;
        scale.x = targetDirection.x > 0 ? 1 : -1;
        transform.localScale = scale;
        body.MovePosition(transform.position + targetDirection * speed);
    }
}
