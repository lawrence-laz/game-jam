using UnityEngine;

public class SoundPlayer : Singleton<SoundPlayer>
{
    [SerializeField]
    AudioClip melody1;
    [SerializeField]
    AudioClip melody2;
    [SerializeField]
    AudioClip melody3;
    [SerializeField]
    AudioClip calling;
    [SerializeField]
    AudioClip answer;
    [SerializeField]
    AudioClip low;
    [SerializeField]
    AudioClip plug;

    public void PlayMelody1()
    {
        GetComponent<AudioSource>().PlayOneShot(melody1);
    }

    public void PlayMelody2()
    {
        GetComponent<AudioSource>().PlayOneShot(melody2);
    }

    public void PlayMelody3()
    {
        GetComponent<AudioSource>().PlayOneShot(melody3);
    }

    public void PlayCalling()
    {
        GetComponent<AudioSource>().PlayOneShot(calling);
    }
    public void PlayAnswer()
    {
        GetComponent<AudioSource>().PlayOneShot(answer);
    }
    public void PlayLow()
    {
        GetComponent<AudioSource>().PlayOneShot(low);
    }
    public void PlayPlug()
    {
        GetComponent<AudioSource>().PlayOneShot(plug);
    }
}
