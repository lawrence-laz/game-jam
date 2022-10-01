using DG.Tweening;
using UnityEngine;

public class TimerBackground : MonoBehaviour
{
    public int AnimationIndex = 0;
    public Vector2 FromEven;
    public Vector2 ToEven;
    public Vector2 FromOdd;
    public Vector2 ToOdd;
    
    void Start()
    {
        FindObjectOfType<Timer>().OnStarted.AddListener(BackgroundAnimationStep);
        FindObjectOfType<Timer>().OnTenSeconds.AddListener(BackgroundAnimationStep);
    }

    private void BackgroundAnimationStep()
    {
        if (AnimationIndex % 2 == 0)
        {
            // Cover screen from above
            transform.position = FromEven;
            transform.DOMove(ToEven, 9.9f);
        }
        else
        {
            // Uncover screen to bottom
            transform.position = FromOdd;
            transform.DOMove(ToOdd, 9.9f);
        }
        AnimationIndex++;
    }
}
