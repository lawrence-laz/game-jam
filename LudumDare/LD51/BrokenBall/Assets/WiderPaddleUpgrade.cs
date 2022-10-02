using UnityEngine;

public class WiderPaddleUpgrade : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        FindObjectOfType<Paddle>().transform.Find("Normal").gameObject.SetActive(false);
        FindObjectOfType<Paddle>().transform.Find("Long").gameObject.SetActive(true);
    }
}
