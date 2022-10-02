using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Libs.Base.Effects;
using UnityEngine;

public class PaddleEnemy : MonoBehaviour
{
    public Tween Tween;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Paddle")
        {
            transform.DOKill();
            transform.parent?.DOKill();
            Tween?.Kill();

            var sprites = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                var spriteFlash = sprite.GetComponent<SpriteFlash>();
                if (spriteFlash == null)
                {
                    spriteFlash = sprite.gameObject.AddComponent<SpriteFlash>();
                }

                spriteFlash.Blink();
            }

            FindObjectOfType<GameOver>().InvokeCollidedWithObstacle(other.contacts[0].point);
        }
    }
}
