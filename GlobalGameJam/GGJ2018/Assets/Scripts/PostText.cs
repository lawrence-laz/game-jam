using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Post Name", menuName = "FEED/Post TEXT", order = 0)]
public class PostText : ScriptableObject
{
    public PostUser Author;
    [TextArea(0, 20)]
    public string Text;
}
