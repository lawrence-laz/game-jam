using DG.Tweening;
using System.Threading;
using UnityEngine;

public class WeaponDamageSystem : MonoBehaviour
{
    private SfxrSynth _hitSound;

    private void OnEnable()
    {
        _hitSound = new SfxrSynth();
        _hitSound.parameters.SetSettingsString("3,.5,,.0864,,.1501,.3,.5487,,-.6497,,,,,,,,,,,,,,,,1,,,.2633,,,");
    }

    public void HandleOnDamagedByWeapon(WeaponComponent weapon, HealthComponent target)
    {
        Vector3 hitDirection = target.transform.position - weapon.transform.position;
        var body = target.GetComponent<Rigidbody2D>();
        if (body != null)
            body.AddForce(hitDirection.normalized * weapon.PushPower);

        StunWalking(target.GetComponent<MovementComponent>());

        target.Health = Mathf.Max(0, target.Health - weapon.Damage);
        _hitSound.PlayMutated();

        var flash = target.GetComponentInChildren<SpriteFlash>();
        if (flash != null)
        {
            DOTween.Sequence()
                .AppendCallback(() => flash.WhiteSprite())
                .Append(Camera.main.transform.DOShakePosition(0.1f, 0.1f))
                .AppendCallback(() => flash.NormalSprite());
        }
        Thread.Sleep(30);
    }

    private void StunWalking(MovementComponent move)
    {
        if (move == null)
            return;

        move.SlowDown = 0.1f;
    }
}
