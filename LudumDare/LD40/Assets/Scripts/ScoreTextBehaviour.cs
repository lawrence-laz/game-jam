using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTextBehaviour : MonoBehaviour
{
    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = Time.timeSinceLevelLoad.ToString("0");
    }
}
