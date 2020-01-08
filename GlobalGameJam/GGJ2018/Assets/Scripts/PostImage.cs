using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Post Name", menuName = "FEED/Post IMAGE", order = 0)]
public class PostImage : ScriptableObject
{
    public PostUser Author;
    [TextArea(5, 20)]
    public string Text;
    public Sprite Image;
}
