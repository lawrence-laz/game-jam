using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public Text Text { get; private set; }
    public Score Score { get; private set; }

    private void Start()
    {
        Text = GetComponent<Text>();
        Score = FindObjectOfType<Score>();
        Text.text = Text.text
            .Replace("{SCORE}", Score.Current.ToString())
            .Replace("{BEST}", Score.Best.ToString())
            ;

        Score.Best = Score.Current > Score.Best ? Score.Current : Score.Best;
    }
}
