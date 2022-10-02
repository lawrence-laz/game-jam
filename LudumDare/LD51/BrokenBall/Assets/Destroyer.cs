using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Ball>() != null)
        {
            GlobalAudio.Instance.Play(GlobalAudio.Instance.LostBall);
        }
        Debug.Log($"Destroyer destroying: {other.gameObject}");
        Destroy(other.gameObject);
    }
}
