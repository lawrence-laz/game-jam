using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Badges
{
    Pentagram,
    Fire
}

public class Badge : MonoBehaviour
{
    public Badges BadgeName;

    private Image _image;

    private void Start() {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _image.color = Get(BadgeName)
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
