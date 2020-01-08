using DG.Tweening;
using UnityEngine;

public class TargetAttackSystem : MonoBehaviour
{
    private SfxrSynth _hugeGuyScream;

    private void OnEnable()
    {
        _hugeGuyScream = new SfxrSynth();
        _hugeGuyScream.parameters.SetSettingsString("3,.507,.111,.347,,.384,.352,.061,,.03,.035,.023,.145,.038,,,-.005,,,,,,,.1456,-.183,.741,-.01,,.101,,.661,-.01");
    }

    private void Update()
    {
        if (GameOverSystem.Instance.GameOver || VictorySystem.Instance.Victory)
            return;

        foreach (var attackComponent in FindObjectsOfType<TargetAttackComponent>())
        {
            HealthComponent health = attackComponent.GetComponent<HealthComponent>();
            WeaponComponent weapon = attackComponent.GetComponentInChildren<WeaponComponent>();

            if (health.Health <= 0)
                continue;

            // Aim and face
            Vector3 targetPosition = attackComponent.IsAttacking ? attackComponent.TargetPosition : attackComponent.TargetTransform.position;
            weapon.transform.LookTowards2D(targetPosition, 2);
            Vector3 facingDirection = targetPosition - attackComponent.transform.position;
            Transform spriteTransform = attackComponent.GetComponentInChildren<DeathComponent>().transform;
            Vector3 scale = spriteTransform.localScale;
            scale.x = (facingDirection.x < 0 ? 1 : -1);
            spriteTransform.transform.localScale = scale;

            if (!attackComponent.IsAttacking
                && weapon.IsReady
                && attackComponent.transform.DistanceTo(attackComponent.TargetTransform) <= weapon.Range)
            {
                attackComponent.TargetPosition = attackComponent.TargetTransform.position;
                Attack(attackComponent, weapon);
            }
        }
    }

    private void Attack(TargetAttackComponent target, WeaponComponent weapon)
    {
        Transform weaponSprite = weapon.transform.Find("Sword_Sprite");
        target.IsAttacking = true;

        if (weapon.transform.parent.name.Contains("Huge") && Random.value > 0.4f)
        {
            _hugeGuyScream.PlayMutated();
            Camera.main.transform.DOShakePosition(0.5f, 0.4f, 40, 0, false, true);
        }

        DOTween.Sequence()
            .Append(weaponSprite.DOLocalMove(weapon.InitialPosition, 0.1f))
            .AppendCallback(() => weaponSprite.GetComponent<Collider2D>().enabled = true)
            .AppendCallback(() => weapon.LastTimeAttackedAt = Time.time)
            .Append(weaponSprite.DOLocalMove(Vector3.up * weapon.Range, Mathf.Min(weapon.Speed, 0.15f)))
            .AppendCallback(() => weaponSprite.GetComponent<Collider2D>().enabled = false)
            .Append(weaponSprite.DOLocalMove(Vector3.up * weapon.Range, weapon.HitDelay))
            .AppendCallback(() => target.IsAttacking = false)
            .Append(weaponSprite.DOLocalMove(weapon.InitialPosition, weapon.Speed * 2))
            .Play();
    }
}
