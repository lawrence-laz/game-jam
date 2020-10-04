using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
    public float StepDuration;

    public Transform HoursArrow;
    public Transform MinutesArrow;

    public TimeSpan Time { get; set; } = DateTime.Now.TimeOfDay;
    public bool IsDark => Time.Hours > 20 || Time.Hours < 6;
    public bool IsNight => Time.Hours >= 0 && Time.Hours < 6;
    public bool IsWorkHours => Time.Hours >= 7 && Time.Hours <= 15;

    public List<Func<Tween>> OnHourPassedTween = new List<Func<Tween>>();
    public UnityEvent OnHourPassed;

    public Tween IncreaseHours(int hours)
    {
        var onHourPassedEffects = OnHourPassedTween.Select(x => x.Invoke()).Where(x => x != null).ToArray();

        var sequence = DOTween.Sequence()
            .Append(DOTween.To(() => Time.TotalMilliseconds, x => Time = TimeSpan.FromMilliseconds(x), TimeSpan.FromHours(hours).TotalMilliseconds, StepDuration)
                .SetRelative(true)
                .SetEase(Ease.Linear)
            )
            .AppendCallback(() => OnHourPassed.Invoke());

        foreach (var effect in onHourPassedEffects)
        {
            sequence.Append(effect);
        }

        return sequence;
    }

    [ContextMenu("Set Midnight")]
    public void SetMidnight()
    {
        Time += TimeSpan.FromHours(24 - Time.Hours);
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
