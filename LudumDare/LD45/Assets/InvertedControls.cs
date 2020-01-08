using UnityEngine;

public class InvertedControls : MonoBehaviour
{
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.F2))
        {
            PlayerPrefs.SetInt("invert y", PlayerPrefs.GetInt("invert y", 1) * -1);
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.F3))
        {
            PlayerPrefs.SetInt("invert x", PlayerPrefs.GetInt("invert x", 1) * -1);
        }
    }
}
