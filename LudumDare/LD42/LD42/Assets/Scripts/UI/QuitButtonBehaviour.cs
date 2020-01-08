using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitButtonBehaviour : MonoBehaviour
{
    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveListener(QuitApplication);
        button.onClick.AddListener(QuitApplication);
    }

    private void QuitApplication()
    {
        Application.Quit();
    }
}
