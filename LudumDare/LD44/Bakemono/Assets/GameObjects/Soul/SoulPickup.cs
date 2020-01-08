using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class SoulPickup : MonoBehaviour
{
    public UnityEvent OnPickup;
    
    public static float PickUpDelay = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PickupForHeroe(collision);
    }

    private void Update()
    {
        PickUpDelay = Mathf.Max(0, PickUpDelay - Time.deltaTime);
    }

    private void PickupForHeroe(Collision2D collision)
    {
        if (collision.collider.tag != "Player")
            return;

        RepeatedAudio.Get("SoulPickup_Audio").PlayTimes++;
        OnPickup.Invoke();
        enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        var soulText = GameObject.Find("HeroHealth_Text").transform;
        var targetPosition = Camera.main.ScreenToWorldPoint(soulText.position);
        PickUpDelay += 0.1f;
        DOTween.Sequence()
            .Append(transform.DOMove(targetPosition, .5f + PickUpDelay))
            .AppendCallback(() =>
            {
                var stats = collision.collider.GetComponent<HeroStats>();
                stats.Health += 5;
                Destroy(gameObject);
            });
    }
}
