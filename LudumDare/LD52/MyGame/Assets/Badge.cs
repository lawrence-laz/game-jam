using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Badges
{
    Pentagram,
    Fire
}

public class Badge : MonoBehaviour
{
    public Badges BadgeName;

    private SpriteRenderer _spriteRenderer;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.color = Get(BadgeName)
            ? Color.white
            : Color.black;
    }

    public static void Set(Badges badge)
    {
        PlayerPrefs.SetInt($"Badge_{badge}", 1);
    }

    public static bool Get(Badges badge)
    {
        return PlayerPrefs.GetInt($"Badge_{badge}", 0) == 1;
    }
}
