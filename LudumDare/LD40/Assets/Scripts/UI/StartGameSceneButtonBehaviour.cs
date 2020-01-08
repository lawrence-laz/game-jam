using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameSceneButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool startMainMenu = false;

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (startMainMenu)
        {
            button.onClick.AddListener(StartMainMenu);
        }
        else
        {
            button.onClick.AddListener(StartGameScene);
        }
    }

    private void StartGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    private void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}