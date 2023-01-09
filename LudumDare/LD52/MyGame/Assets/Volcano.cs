using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<ScreenShake>().MediumShake();
        FindObjectOfType<PlayerController>().transform.position += Vector3.right * 0.5f;
    }
}
