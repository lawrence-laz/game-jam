using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public abstract string Text { get; }
    public AudioClip AudioClip;

    public abstract bool CanInvoke(Interactor interactor, GameObject target);

    public Sequence Invoke(Interactor interactor, GameObject target, bool silent = false)
    {
        var sequence = InnerInvoke(interactor, target);
        if (!silent)
        {
            PlaySound(interactor);
        }
        return sequence;
    }

    public abstract Sequence InnerInvoke(Interactor interactor, GameObject target);

    public void PlaySound(Interactor interactor)
    {
        if (AudioClip == null)
        {
            return;
        }

        var audioSource = interactor.GetComponent<AudioSource>();
        audioSource.PlayOneShot(AudioClip);
    }
}
