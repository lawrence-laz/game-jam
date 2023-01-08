using System.Collections;
using System.Collections.Generic;
using Libs.Base.GameLogic.AudioSource;
using UnityEngine;

public class WalkingSoundsController : MonoBehaviour
{
    public float PlaySoundPeriod = 1;

    private PlayRandomAudioClipBehaviour _audioPlayer;
    private float _unaccountedMovement;
    private Vector3 _previousPosition;

    private void Start()
    {
        _audioPlayer = GetComponent<PlayRandomAudioClipBehaviour>();
        _previousPosition = transform.position;
    }

    private void Update()
    {
        _unaccountedMovement += transform.DistanceTo2D(_previousPosition);
        _previousPosition = transform.position;

        if (_unaccountedMovement / PlaySoundPeriod >= 1f)
        {
            _audioPlayer.PlayRandomAudioClip();
            _unaccountedMovement = 0;
        }
    }
}
