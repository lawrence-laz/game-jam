using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public abstract string Text { get; }
    public AudioClip AudioClip;

    public abstract bool CanInvoke(Interactor interactor, GameObject target);

    public Sequence Invoke(Interactor interactor, GameObject target)
    {
        var sequence = InnerInvoke(interactor, target);
        PlaySound();
        return sequence;
    }

    public abstract Sequence InnerInvoke(Interactor interactor, GameObject target);

    public void PlaySound()
    {
        if (AudioClip == null)
        {
            return;
        }

        var audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(AudioClip);
    }
}
