using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAudio : MonoBehaviour
{
    private AudioSource _audio;

    public AudioClip OnScore;
    public AudioClip OnComboScore;

    [Header("Internals")]
    public float Combo;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void PlayScored()
    {
        Combo++;
        Debug.Log("Combo: " + Combo);
        if (Combo >= 2.5f)
        {
            _audio.PlayOneShot(OnComboScore);
            FindObjectOfType<ScreenShake>().MediumShake();
            Combo = 0;
        }
        else
        {
            _audio.PlayOneShot(OnScore);
        }
    }

    private void Update()
    {
        Combo = Mathf.Clamp(Combo - Time.deltaTime, 0, 4);
    }
}
