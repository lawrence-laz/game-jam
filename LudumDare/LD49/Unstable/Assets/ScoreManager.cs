using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CurrentScore;
    public int BestScore { get => PlayerPrefs.GetInt("SCORE"); set => PlayerPrefs.SetInt("SCORE", value); }

    private void OnEnable()
    {
        CurrentScore = 0;
    }
}
