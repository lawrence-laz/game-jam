using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenAnimation : MonoBehaviour
{
    public Sequence Run(TweenCallback logic)
    {
        var y = transform.localPosition.y;
        return DOTween.Sequence()
            .Append(transform.DOLocalMoveY(-0.2f, 0.1f).SetRelative(true).SetEase(Ease.InBack))
            .Append(transform.DOLocalMoveY(0.1f, 0.1f).SetRelative(true).SetEase(Ease.InBack))
            .AppendInterval(0.15f)
            .AppendCallback(logic)
            .Append(transform.DOLocalMoveY(0.4f, 0.06f).SetRelative(true).SetEase(Ease.OutBack))
            .Append(transform.DOLocalMoveY(y, 0.1f).SetEase(Ease.Linear))
            .Play();
    }
}
