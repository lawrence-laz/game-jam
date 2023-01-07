using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepLocalRotation : MonoBehaviour
{
    private Quaternion _originalRotation;

    private void Start()
    {
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = _originalRotation;
    }
}
