using UnityEngine;

public class CursorShow : MonoBehaviour
{
    void Start()
    {
        CursorHelper.Instance.ShowCursor = true;
    }
}
