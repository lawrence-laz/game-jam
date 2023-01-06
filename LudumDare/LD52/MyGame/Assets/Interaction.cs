using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    public abstract string Text { get; }

    public abstract void Invoke(Interactor interactor, GameObject target);
}
