using DG.Tweening;
using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float StepDuration;

    public Transform HoursArrow;
    public Transform MinutesArrow;

    public TimeSpan Time { get; set; } = DateTime.Now.TimeOfDay;

    public Tween IncreaseHours(int hours)
    {
        return DOTween.To(
            () => Time.TotalMilliseconds, 
            x => Time = TimeSpan.FromMilliseconds(x), 
            TimeSpan.FromHours(hours).TotalMilliseconds, 
            StepDuration)
            .SetRelative(true);
    }

    public void ResetStepDuration()
    {
        StepDuration = 2;
    }

    private void Start()
    {
        ResetStepDuration();
    }

    private void Update()
    {
        UpdateArrows();
    }

    private void UpdateArrows()
    {
        var minutesRotation = (float)Time.Minutes / 60;
        var hoursRotation = ((Time.Hours % 12) + minutesRotation) / 12;

        HoursArrow.rotation = Quaternion.Euler(0, 0, hoursRotation * -360);
        MinutesArrow.rotation = Quaternion.Euler(0, 0, minutesRotation * -360);
    }
}
