using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSorter : MonoBehaviour
{
    private static readonly float OffsetRange = 0.1f;
    private static readonly Vector2 YRange = new Vector2(0, 3);
    private float _startingZ;

    private void Start()
    {
        _startingZ = transform.localPosition.z;
    }

    private void Update()
    {
        var currentPosition = transform.localPosition;
        var offsetLerpValue = Mathf.InverseLerp(YRange.x, YRange.y, transform.position.y); // Global position here
        currentPosition.z = _startingZ + Mathf.Lerp(0, OffsetRange, offsetLerpValue);
        transform.localPosition = currentPosition;
    }
}
