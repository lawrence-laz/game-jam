using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public bool KeepY;

    private void Update()
    {
        if (Target != null)
        {
            var originalY = transform.position.y;
            var newPosition = Target.position + Offset;
            if (KeepY)
            {
                newPosition.y = originalY;
            }
            transform.position = newPosition;
        }
    }
}
