using DG.Tweening;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    private SfxrSynth _deathSound;
    private SfxrSynth _hugeGuyScream;

    private void OnEnable()
    {
        _deathSound = new SfxrSynth();
        _deathSound.parameters.SetSettingsString("3,.574,,.2422,.6861,.2493,.3,.022,,-.0679,,,,,,,,,,,,,,-.1361,-.0574,1,,,,,,");
        _hugeGuyScream = new SfxrSynth();
        _hugeGuyScream.parameters.SetSettingsString("3,.507,.111,.347,,1,.352,.061,,.03,.035,.023,.145,.038,,,-.005,,,,,,,.1456,-.183,.741,-.01,,.101,,.661,-.01");
    }

    private void Start()
    {
        StartSystem();
    }

    public void StartSystem()
    {
        InvokeRepeating("CheckForDead", 0, 0.1f);
    }

    private void CheckForDead()
    {
        foreach (var health in FindObjectsOfType<HealthComponent>())
        {
            if (health.Health > 0)
                continue;

            DeathComponent death = health.GetComponentInChildren<DeathComponent>();
            if (death == null)
                continue;

            if (death.DeathSprite == null)
            {
                Debug.LogWarning("Death sprite not set");
                continue;
            }

            SpriteRenderer spriteRenderer = death.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning("Shouldn't happen");
                continue;
            }

            if (spriteRenderer.sprite == death.DeathSprite)
                continue; // Done with this one

            if (health.transform.name.Contains("Huge"))
            {
                _hugeGuyScream.PlayMutated();
                Camera.main.transform.DOShakePosition(1.5f, 0.4f, 40, 0, false, true);
            }
            else
            {
                _deathSound.PlayMutated();
            }

            spriteRenderer.sprite = death.DeathSprite;

            SwitchParticles(health.GetComponentsInChildren<ParticleSystem>());

            if (!health.IsOutOfBounds)
                ThrowWeapon(death);
        }
    }

    private void SwitchParticles(ParticleSystem[] particles)
    {
        if (particles == null)
            return;

        foreach (var particle in particles)
        {
            var hp = particle.GetComponentInParent<HealthComponent>();

            if (particle.isPlaying)
            {
                particle.Stop();
            }
            else if (hp != null && hp.IsOutOfBounds == false)
            {

                particle.Play();
            }
        }
    }

    private void ThrowWeapon(DeathComponent death)
    {
        if (death.Weapon == null)
            return;

        var colliders = death.Weapon.GetComponentsInChildren<Collider2D>();
        if (colliders != null && colliders.Length > 0)
        foreach (var collider in colliders)
        {
            collider.isTrigger = false;
        }

        death.Weapon.SetParent(null, true);
        DOTween.Sequence()
            .Append(death.Weapon.DOMove(death.Weapon.position + Vector3.up, 0.4f))
            .Join(DOTween.Sequence()
                    .Append(death.Weapon.DORotate(Vector3.forward * 360, .2f, RotateMode.WorldAxisAdd))
                    .Append(death.Weapon.DORotate(Vector3.forward * 0, .2f, RotateMode.WorldAxisAdd)))
            .Append(death.Weapon.DORotate(Vector3.forward * 180, .2f, RotateMode.WorldAxisAdd))
            .Join(death.Weapon.DOMove(death.Weapon.position, 0.2f))
            .Play();
    }
}
