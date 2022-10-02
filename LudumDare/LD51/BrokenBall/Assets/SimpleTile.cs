using DG.Tweening;
using Libs.Base.Effects;
using UnityEngine;

public class SimpleTile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Destroy(gameObject);
        if (other.gameObject.GetComponent<Ball>())
        {
            foreach (var flash in gameObject.GetComponentsInChildren<SpriteFlash>())
            {
                flash.Blink();
            }

            var score = FindObjectOfType<Highscore>();
            if (score != null)
            {
                score.CurrentScore++;
            }
            else
            {
                Debug.Log("Couldn't find scopre object!");
            }
        }
    }

    private void OnDestroy() {
        transform.DOKill();
    }
}
