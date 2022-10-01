using UnityEngine;

public class SimpleTile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (other.gameObject.GetComponent<Ball>())
        {
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
}
