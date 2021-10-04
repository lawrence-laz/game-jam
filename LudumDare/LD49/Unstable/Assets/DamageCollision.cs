using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class DamageCollision : MonoBehaviour
{
    public float Damage = 45;
    public UnityEvent OnHit;
    public bool HasDamaged = false;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = null;
        if (collision.rigidbody?.TryGetComponent(out player) != true || HasDamaged)
        {
            return;
        }
        
        var perp = Vector3.Cross(player.transform.forward, collision.contacts[0].normal);
        float dir = Vector3.Dot(perp, player.transform.up);
        player.transform.DOLocalRotate(Vector3.forward * Damage * Mathf.Sign(dir), 0.2f).SetRelative();
        //player.transform.Rotate(Vector3.forward * Damage * GetDirection(player.transform, transform), Space.Self);
        FindObjectOfType<ScreenShake>().MediumShake();
        OnHit.Invoke();
        HasDamaged = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerController>(out var player) || HasDamaged)
        {
            return;
        }

        player.transform.DOLocalRotate(Vector3.forward * Damage * GetDirection(player.transform, transform), 0.2f).SetRelative();
        //player.transform.Rotate(Vector3.forward * Damage * GetDirection(player.transform, transform), Space.Self);
        FindObjectOfType<ScreenShake>().MediumShake();
        OnHit.Invoke();
        HasDamaged = true;
    }

    private float GetDirection(Transform player, Transform target)
    {
        var forward = player.forward;
        var up = player.up;
        var targetDir = player.DirectionTo(target);
        var perp = Vector3.Cross(forward, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f; // Right
        }
        else if (dir < 0f)
        {
            return -1f; // Left
        }
        else
        {
            return 0f;
        }
    }
}
