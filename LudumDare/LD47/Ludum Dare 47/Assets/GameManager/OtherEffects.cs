using DG.Tweening;
using System;
using UnityEngine;

public class OtherEffects : MonoBehaviour
{
    public GameObject Sleeping;
    public Clock Clock { get; private set; }
    public Calendar Calendar { get; private set; }

    public bool WorkdAccountedForToday => _lastWorkAccountedDay == Calendar.Day;

    private int _lastWorkAccountedDay = 0;

    private void Start()
    {
        Clock = FindObjectOfType<Clock>();
        Clock.OnHourPassedTween.Add(OnHourPassedTween);
        Clock.OnHourPassed.AddListener(OnHourPassed);
        Calendar = FindObjectOfType<Calendar>();
        Calendar.OnNewDay.AddListener(OnNewDay);
        FindObjectOfType<WorkCard>().GetComponent<Card>().OnActivated.AddListener(OnWork);
    }

    private void OnHourPassed()
    {
        HandleMissedWork();
    }

    private void OnWork()
    {
        _lastWorkAccountedDay = Calendar.Day;
    }

    private void OnNewDay()
    {
        //_lastWorkAccountedDay = false;
    }

    private Tween OnHourPassedTween()
    {
        var sequence = DOTween.Sequence();

        var awakeEffect = HandleStayingAwakeAtNightEffect();
        if (awakeEffect != null)
        {
            sequence.Append(awakeEffect);
        }

        return sequence;
    }

    private Tween HandleMissedWork()
    {
        var stats = FindObjectOfType<Stats>();
        if (Clock.Time.Hours > 18 && !Calendar.IsWeekend && (Calendar.Day - _lastWorkAccountedDay) >= 1)
        {
            _lastWorkAccountedDay = Calendar.Day;
            Debug.Log("Call from boss!!!");
            //Debug.Break();
            // TODO: Add chance.

            var face = FindObjectOfType<Face>();
            var bossCall = DOTween.Sequence()
                .AppendCallback(() => Debug.Log("Phone ringing!"))
                .AppendInterval(1)
                .AppendCallback(() => face.SetFace(face.BossCall))
                .AppendInterval(3)
                .Append(DOTween.To(() => stats.Stress, x => stats.Stress = x, -0.6f, 0.1f).SetRelative(true))
                .AppendCallback(() => FindObjectOfType<Tooltip>().ImportantNote = "Increased <color=red>stress</color> due to mising work.")
                .AppendInterval(1f)
                .AppendCallback(() => face.ResetFace())
                .Pause();

            FindObjectOfType<GameManager>().NextEvent = bossCall;
        }

        return default;
    }

    private Tween HandleStayingAwakeAtNightEffect()
    {
        if (Clock.IsNight && !Sleeping.activeInHierarchy)
        {
            var stats = FindObjectOfType<Stats>();
            var tween = DOTween.To(() => stats.Stress,
                x => { if (Clock.IsNight && !Sleeping.activeInHierarchy) stats.Stress = x; },
                -0.17f, 0.1f)
                .SetRelative(true);
            tween.OnPlay(() =>
            {
                if (!Clock.IsNight || Sleeping.activeInHierarchy)
                {
                    tween.Kill();
                }
                else
                {
                    FindObjectOfType<Tooltip>().ImportantNote = "Increased <color=red>stress</color> due to sleepless night.";
                }
            });

            return tween;
        }

        return default;
    }
}
