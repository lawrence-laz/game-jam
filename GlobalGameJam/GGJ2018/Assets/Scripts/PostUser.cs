using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Post User", menuName = "FEED/User", order = 0)]
public class PostUser : ScriptableObject
{
    public string Name;
    public Sprite ProfilePic;
}
