using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Badges
{
    None,
    Pentagram, // TODO
    Fire,
    Tomatoes,
    Bricks, 
    Mud, 
    Metal, 
    Rust, 
    DeadCrow,
    Worm, 
    Wheat50, // TODO
    Anvil, 
    Hammer, // TODO
    Sand, // TODO
    Glass, // TODO
    Bread,
    Lightbulb, // TODO
    Steam, // TODO water+fire
    Electricity,
    LavaBucket,
    LavaHole,
    Volcano,
    Dust,
}

public class Badge : MonoBehaviour
{
    public Badges BadgeName;

    private Image _image;

    private void Start() {
        _image = GetComponent<Image>();
        Update();
    }

    private void Update()
    {
        _image.color = Get(BadgeName)
            ? Color.white
            : Color.black;
    }

    public static bool Set(Badges badge)
    {
        if (Get(badge))
        {
            return false;
        }

        PlayerPrefs.SetInt($"Badge_{badge}", 1);
        return true;
    }

    public static bool Get(Badges badge)
    {
        return PlayerPrefs.GetInt($"Badge_{badge}", 0) == 1;
    }
}
