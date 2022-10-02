using UnityEngine;

public class WiderPaddleUpgrade : MonoBehaviour
{
    public GameObject ReplacementForWhenPaddleIsAlreadyExpanded;

    private void OnEnable()
    {
        if (FindObjectOfType<Paddle>().transform.Find("Long").gameObject.activeSelf)
        {
            Instantiate(ReplacementForWhenPaddleIsAlreadyExpanded, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Paddle")
        {
            FindObjectOfType<Paddle>().transform.Find("Normal").gameObject.SetActive(false);
            FindObjectOfType<Paddle>().transform.Find("Long").gameObject.SetActive(true);
        }
    }
}
