using UnityEngine;

public class SimpleTile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
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
