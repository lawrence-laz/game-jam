using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLocalRotation : MonoBehaviour
{
    public Quaternion OriginalRotation;

    private void Start()
    {
        OriginalRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
