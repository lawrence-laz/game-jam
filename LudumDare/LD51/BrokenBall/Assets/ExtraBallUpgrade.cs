using UnityEngine;

public class ExtraBallUpgrade : MonoBehaviour
{
    public GameObject NewBallPrefab;

    private void OnCollisionEnter2D(Collision2D _)
    {
        var newBall = Instantiate(NewBallPrefab, FindObjectOfType<Paddle>().transform.position + Vector3.up, Quaternion.identity);
        newBall.GetComponent<Ball>().Launch();
    }
}
