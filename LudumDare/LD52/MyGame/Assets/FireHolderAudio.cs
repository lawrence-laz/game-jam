using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireHolderAudio : MonoBehaviour
{
    private Holder _holder;
    private AudioSource _audioSource;

    private void Start()
    {
        _holder = GetComponentInParent<Holder>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        var isHoldingFlame = _holder.Items.Any(item => item.GetComponent<Label>().Is("flame"));
        if (isHoldingFlame && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        else if (!isHoldingFlame && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
