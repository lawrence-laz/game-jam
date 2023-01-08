using System.Collections;
using System.Collections.Generic;
using Libs.Base.GameLogic.AudioSource;
using UnityEngine;

public class WalkingSoundsController : MonoBehaviour
{
    public float PlaySoundPeriod = 1;
    private SpriteFrameAnimator _animator;
    private PlayRandomAudioClipBehaviour _audioPlayer;
    private float _unaccountedMovement;
    private Vector3 _previousPosition;
    private Movement _movement;

    private void Start()
    {
        _animator = GetComponent<SpriteFrameAnimator>();
        _audioPlayer = GetComponent<PlayRandomAudioClipBehaviour>();
        _previousPosition = transform.position;
        _movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        var deltaMovement = transform.DistanceTo2D(_previousPosition);
        _unaccountedMovement += deltaMovement;
        _previousPosition = transform.position;

        if (_unaccountedMovement / PlaySoundPeriod >= 1f)
        {
            _audioPlayer.PlayRandomAudioClip();
            _animator.NextFrame();
            _unaccountedMovement = 0;
        }
        else if (_movement.Direction == Vector2.zero)
        {
            _animator.StopAnimation();
        }
    }
}
