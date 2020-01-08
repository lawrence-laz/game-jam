using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefStartEvent : MonoBehaviour
{
    public Color BeatenColor;

    private void Start()
    {
        string sceneName = GetComponent<SceneLoadingBehaviour>().SceneName;
        if (PlayerPrefs.GetInt(sceneName, 0) == 1)
        {
            var button = GetComponent<Button>();
            var colors = button.colors;
            colors.normalColor = BeatenColor;
            button.colors = colors;
        }
    }
}
