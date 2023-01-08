using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public abstract string Text { get; }

    public abstract bool CanInvoke(Interactor interactor, GameObject target);

    public abstract Sequence Invoke(Interactor interactor, GameObject target);
}
