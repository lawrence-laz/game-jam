using System.Collections;
using System.Collections.Generic;
using Libs.Base.Effects;
using UnityEngine;

public class PaddleEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Paddle")
        {
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

            FindObjectOfType<GameOver>().Invoke();
        }
    }
}
