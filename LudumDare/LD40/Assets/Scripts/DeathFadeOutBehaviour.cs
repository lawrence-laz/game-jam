using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DeathFadeOutBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private SpriteRenderer sprite;

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Color color = sprite.color;
        color.a = Mathf.MoveTowards(color.a, 0, speed);
        sprite.color = color;

        if (color.a == 0)
        {
            Destroy(gameObject);
        }
    }
}

