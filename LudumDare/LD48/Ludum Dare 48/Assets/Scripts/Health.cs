using UnityEngine;
using UnityEngine.Events;

public class Health : Value
{
    public float MaxValue;
    public float CurrentValue;
    public AudioClip Collision;

    public float RelativeVelocityDamageThreshold;
    public GameObject CollisionFx;

    public UnityEvent OnDead;
    
    public override float NormalizedValue => CurrentValue / MaxValue;

    private bool _onDeadCalled;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CurrentValue = Mathf.Clamp(CurrentValue, 0, MaxValue);

        if (CurrentValue == 0 && !_onDeadCalled)
        {
            _onDeadCalled = true;
            OnDead.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionMagnitude = collision.relativeVelocity.magnitude;
        if (collisionMagnitude < RelativeVelocityDamageThreshold)
        {
            return;
        }

        if (CollisionFx != null)
        {
            Instantiate(CollisionFx, collision.contacts[0].point, Quaternion.identity);
        }
        var damageTaken = Mathf.Lerp(10, 45, Mathf.InverseLerp(RelativeVelocityDamageThreshold, 8, collisionMagnitude));
        if (collision.rigidbody != null)
        {
            damageTaken *= Mathf.Lerp(0.5f, 1f, Mathf.InverseLerp(1, 25, collision.rigidbody.mass));
            if (Collision != null)
                _audio.PlayOneShot(Collision);
        }
        CurrentValue -= damageTaken;

        if (transform.CompareTag("Player"))
        {
            ScreenShake.Get().MediumShake();
        }

        Debug.Log($"Collision magnitude: {collisionMagnitude}; damage: {damageTaken}");
    }
}
