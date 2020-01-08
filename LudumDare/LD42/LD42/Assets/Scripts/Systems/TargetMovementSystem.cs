using UnityEngine;

public class TargetMovementSystem : MonoBehaviour
{
    private TargetMovementComponent[] _targets;

    private void Start()
    {
        _targets = FindObjectsOfType<TargetMovementComponent>();
    }

    private void FixedUpdate()
    {
        if (GameOverSystem.Instance.GameOver || VictorySystem.Instance.Victory)
            return;

        foreach (var target in _targets)
        {
            var hp = target.GetComponent<HealthComponent>();
            var targetAtk = target.GetComponent<TargetAttackComponent>();

            if (hp != null && hp.Health <= 0)
                continue;

            if (targetAtk != null && targetAtk.IsAttacking == true)
                continue;

            Rigidbody2D body = target.GetComponent<Rigidbody2D>();
            MovementComponent movement = target.GetComponent<MovementComponent>();
            Vector3 targetPosition = target.TargetTransform == null ? target.TargetPosition : target.TargetTransform.position;
            Vector2 targetDirection = (targetPosition - target.transform.position).normalized;
            //body.MovePosition(body.position + targetDirection * movement.Speed); // AI will use force to move around.
            body.AddForce(targetDirection * movement.ActualSpeed * 700);
        }
    }
}
