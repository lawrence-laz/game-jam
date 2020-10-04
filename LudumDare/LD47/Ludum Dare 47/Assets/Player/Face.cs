using UnityEngine;

public class Face : MonoBehaviour
{
    public GameObject CurrentFace;

    public Clock Clock { get; private set; }
    public Stats Stats { get; private set; }

    [Header("Faces")]
    public GameObject Default;
    public GameObject Smiling;
    public GameObject VeryExcided;
    public GameObject Intense;
    public GameObject Sleep;
    public GameObject GameOver;
    public GameObject ShakyHappy;
    public GameObject Focused;
    public GameObject Sleepy;
    public GameObject Eating;
    public GameObject BossCall;

    public void ResetFace()
    {
        SetFace(Default);
    }

    public void SetFace(GameObject face)
    {
        if (CurrentFace == face)
        {
            return;
        }

        CurrentFace.SetActive(false);
        CurrentFace = face;
        CurrentFace.SetActive(true);
    }

    private void Start()
    {
        CurrentFace = Default;
        Clock = FindObjectOfType<Clock>();
        Stats = FindObjectOfType<Stats>();
    }

    private void Update()
    {
        if (Clock.IsNight)
        {
            Default = Sleepy;
        }
        else if (Stats.Hunger < 0.35f || Stats.Stress < 0.35f)
        {
            Default = Intense;
        }
        else if (Stats.Energy <= 2 || Stats.Fun < 0.35f)
        {
            Default = Sleepy;
        }
        else
        {
            Default = Smiling;
        }
    }
}
