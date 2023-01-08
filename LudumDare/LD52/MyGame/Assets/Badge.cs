using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Badges
{
    None,
    Pentagram, // TODO
    Fire,
    Tomatoes, // TODO
    Bricks, // TODO
    Mud, // TODO
    Metal, // TODO
    Rust, // TODO
    DeadCrow, // TODO
    Worm, // TODO
    Wheat50, // TODO
    Anvil, // TODO
    Hammer, // TODO
    Sand, // TODO
    Glass, // TODO
    Bread, // TODO
    Lightbulb, // TODO
    Steam, // TODO water+fire
    Electricity, // TODO water+fire
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
