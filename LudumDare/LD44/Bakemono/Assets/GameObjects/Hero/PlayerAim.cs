using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private void Update()
    {
        UpdateAimRotationBasedOnMousePosition();
    }

    private void UpdateAimRotationBasedOnMousePosition()
    {
        transform.LookAt2D(InputX.MouseWorldPosition);
    }
}
