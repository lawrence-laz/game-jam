using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletedTextBehaviour : MonoBehaviour
{
    public Color CompletedColor;
    public int LevelIndex;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        if (PlayerPrefs.GetInt("Completed" + LevelIndex, 0) != 0)
        {
            Debug.LogFormat("{0} is completed", LevelIndex);
            _text.color = CompletedColor;
        }
    }

    public static void CompleteCurrentLevel()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Completed" + index, 1);
    }
}
