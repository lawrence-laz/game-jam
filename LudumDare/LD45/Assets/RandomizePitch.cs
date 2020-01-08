using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePitch : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().pitch += Random.Range(-.2f, 3f);
    }
}
