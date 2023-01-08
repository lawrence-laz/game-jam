using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Libs.Base.Extensions;
using UnityEngine;

public class DigHoleInteraction : Interaction
{
    public override string Text => "Dig a hole";
    public GameObject HolePrefab;

    public override bool CanInvoke(Interactor interactor, GameObject target)
    {
        // TextTerm.Set("{held}", GetComponent<Label>()?.Text);
        return true;
    }

    public override Sequence InnerInvoke(Interactor interactor, GameObject target)
    {
        var holder = interactor.GetComponentInChildren<Holder>();
        if (holder == null)
        {
            return null;
        }

        var result = Instantiate(HolePrefab);
        result.transform.position = transform.position;
        result.EnableAllComponentsInChildren<Collider2D>();
        return DOTween.Sequence()
            .AppendInterval(2)
            .Play();
    }
}
