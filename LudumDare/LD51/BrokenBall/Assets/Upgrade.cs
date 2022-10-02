using System.Diagnostics;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject ReplacementForWhenPaddleIsAlreadyExpanded;
    public Vector2 Velocity;

private void OnEnable() {
    if (FindObjectOfType<Paddle>().transform.Find("Long").gameObject.activeSelf)
    {
        Instantiate(ReplacementForWhenPaddleIsAlreadyExpanded, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

    void Update()
    {
        transform.position += (Vector3)Velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GlobalAudio.Instance.Play(GlobalAudio.Instance.Upgrade);
        Destroy(gameObject);
    }
}
