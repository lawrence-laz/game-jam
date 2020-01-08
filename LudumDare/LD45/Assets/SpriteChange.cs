using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    public SpriteRenderer Renderer { get; set; }
    public Sprite Sprite1;
    public Sprite Sprite2;

    protected virtual void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite1()
    {
        Renderer.sprite = Sprite1;
    }

    public void SetSprite2()
    {
        Renderer.sprite = Sprite2;
    }
}
