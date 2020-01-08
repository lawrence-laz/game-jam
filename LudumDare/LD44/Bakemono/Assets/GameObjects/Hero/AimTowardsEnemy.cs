using UnityEngine;

public class AimTowardsEnemy : MonoBehaviour
{
    public Transform Target;
    public LayerMask TargetMask;
    public float AssistRatio = 500;

    private void Update()
    {
        if (Target == null)
            LookForTarget();

        if (Target == null)
            return;

        transform.LookTowards2D(Target.position, AssistRatio * Time.deltaTime);
    }

    private void LookForTarget()
    {
        var result = Physics2D.CircleCast(
            transform.position,
            2, 
            InputX.MouseWorldPosition - transform.position, 
            20,
            TargetMask);

        if (result.transform == null)
            return;

        Debug.Log("<color=#f0f>Target Found</color>");

        Target = result.transform;
    }
}
