using System;
using UnityEngine;
using UnityEngine.Events;

public class Calendar : MonoBehaviour
{
    public TextMesh CurrentNumberText;
    public TextMesh CurrentText;
    public GameObject NotePrefab;

    public UnityEvent OnNewDay;

    public int Day = 1;

    public bool IsWeekend => Day % 7 == 0 || Day % 7 == 6;

    public Clock Clock { get; private set; }

    private void Start()
    {
        Clock = FindObjectOfType<Clock>();
        UpdateCurrentText();
    }

    private void Update()
    {
        int clockDays = Clock.Time.Days + 1;
        if (clockDays != Day)
        {
            var note = Instantiate(NotePrefab, transform.position - Vector3.forward * 0.05f, transform.rotation);
            var noteNumberText = note.transform.Find("Quad/NumberText").GetComponentInChildren<TextMesh>();
            var noteText = note.transform.Find("Quad/Text").GetComponentInChildren<TextMesh>();
            noteNumberText.text = Day.ToString();
            noteNumberText.color = CurrentNumberText.color;
            noteText.text = CurrentText.text;
            noteText.color = CurrentText.color;
            Day = clockDays;
            UpdateCurrentText();

            OnNewDay.Invoke();
        }
    }

    private void UpdateCurrentText()
    {
        CurrentNumberText.text = Day.ToString();
        CurrentNumberText.color = IsWeekend ? Color.red : Color.black;
        CurrentText.text = IsWeekend ? "Weekend" : "Workday";
        CurrentText.color = IsWeekend ? Color.red : Color.black;
    }
}
