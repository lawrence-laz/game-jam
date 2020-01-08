using DG.Tweening;
using UnityEngine;

public class PlayerMoveAnimation : MonoBehaviour
{
    PlayerMove _move;
    Sequence _anim;
    Quaternion _originalRot;
    Vector3 _originalPos;

    private void OnEnable()
    {
        _move = GetComponentInParent<PlayerMove>();
        _move.OnStartedMove.AddListener(StartAnimation);
        _move.OnStoppedMove.AddListener(StopAnimation);
    }

    private void StopAnimation()
    {
        KillAnim();
        transform.localRotation = _originalRot;
        transform.localPosition = _originalPos;
    }

    private void StartAnimation()
    {
        _originalRot = transform.localRotation;
        _originalPos = transform.localPosition;
        var str = 6.5f;
        var jump = 0.1f;
        var dur = 0.13f;
        _anim = DOTween.Sequence()
            .Append(transform.DOLocalRotate(Vector3.forward * str, dur / 3).SetRelative(true))
            .Join(transform.DOLocalMoveY(jump, dur * 2 /3).SetRelative(true))
            .Append(transform.DOLocalRotate(Vector3.forward * -str, dur / 3).SetRelative(true))
            .Join(transform.DOLocalMoveY(-jump, dur * 2 / 3).SetRelative(true))
            .Append(transform.DOLocalRotate(Vector3.forward * -str, dur / 3).SetRelative(true))
            .Join(transform.DOLocalMoveY(jump, dur * 2 / 3).SetRelative(true))
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void KillAnim()
    {
        if (_anim != null)
        {
            _anim.Kill(true);
        }
    }
}
