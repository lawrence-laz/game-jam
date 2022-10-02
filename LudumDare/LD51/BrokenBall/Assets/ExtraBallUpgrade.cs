using UnityEngine;

public class ExtraBallUpgrade : MonoBehaviour
{
    public GameObject NewBallPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Paddle")
        {
            var newBall = Instantiate(NewBallPrefab, FindObjectOfType<Paddle>().transform.position + Vector3.up, Quaternion.identity);
            newBall.GetComponent<Ball>().Launch();
        }
    }
}
