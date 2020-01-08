using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTExt : MonoBehaviour
{
    public Text Text { get; private set; }
    public Score Score { get; private set; }

    private void Start()
    {
        Text = GetComponent<Text>();
        Score = FindObjectOfType<Score>();
    }

    private void Update()
    {
        Text.text = Score.Current.ToString();
    }
}
