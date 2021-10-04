using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private void OnEnable()
    {
        var score = FindObjectOfType<ScoreManager>();
        GetComponent<Text>().text = $@"Enemies defeated: {score.CurrentScore}
Best: {score.BestScore}";
    }
}
