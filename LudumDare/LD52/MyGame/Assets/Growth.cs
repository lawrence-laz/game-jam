using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Libs.Base.Extensions;

public class Growth : MonoBehaviour
{
    public GameObject[] TargetPrefabs;
    public Vector2 DurationRange = new Vector2(1, 2);

    private float _startedGrowingAt;
    private float _willGrowAt;

    private void Start()
    {
        _startedGrowingAt = Time.time;
        _willGrowAt = _startedGrowingAt + Random.Range(DurationRange.x, DurationRange.y);
    }

    private void Update()
    {
        if (Time.time >= _willGrowAt)
        {
            var growth = Instantiate(TargetPrefabs.GetRandom());
            Destroy(gameObject);
            growth.transform.position = transform.position;
        }
    }
}
