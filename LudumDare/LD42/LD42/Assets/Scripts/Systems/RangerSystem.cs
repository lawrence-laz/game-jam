using DG.Tweening;
using UnityEngine;

public class RangerSystem : MonoBehaviour
{
    private void Update()
    {
        if (GameOverSystem.Instance.GameOver || VictorySystem.Instance.Victory)
            return;

        foreach (var ranger in FindObjectsOfType<RangedTargetAttackComponent>())
        {
            var hp = ranger.GetComponent<HealthComponent>();

            if (hp != null && hp.Health <= 0)
                continue;

            AimToTarget(ranger);
            ShootWhenReady(ranger);
        }
    }

    private void ShootWhenReady(RangedTargetAttackComponent ranger)
    {
        if (!ranger.Weapon.ReadyToShootAgain)
            return;

        Transform weaponSprite = ranger.Weapon.transform.GetChild(0);
        GameObject arrow = Instantiate(ranger.Weapon.Arrow, weaponSprite);
        ProjectileComponent projectile = arrow.GetComponent<ProjectileComponent>();
        ranger.Weapon.IsShooting = true;

        DOTween.Sequence()
            .Append(arrow.transform.DOLocalMoveY(arrow.transform.localPosition.y - 0.5f, ranger.Weapon.PreShotDuration))
            .AppendCallback(() =>
            {
                arrow.transform.SetParent(null, true);
                projectile.IsShot = true;
                ranger.Weapon.LastTimeShot = Time.time;
                ranger.Weapon.IsShooting = false;
            });
    }

    private void AimToTarget(RangedTargetAttackComponent ranger)
    {
        // Aim and face
        Vector3 targetPosition = ranger.TargetTransform.position;
        ranger.Weapon.transform.LookTowards2D(targetPosition, 5);
        Vector3 facingDirection = targetPosition - ranger.transform.position;
        Transform spriteTransform = ranger.GetComponentInChildren<DeathComponent>().transform;
        Vector3 scale = spriteTransform.localScale;
        scale.x = (facingDirection.x < 0 ? 1 : -1);
        spriteTransform.transform.localScale = scale;
    }
}
