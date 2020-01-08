using UnityEngine;

public class GroundComponent : MonoBehaviour
{
    public float Size = 1;
    public float Duration;

    public float Width { get { return 2f * Size; } }
    public float Height { get { return 2f * Size; } }
}
