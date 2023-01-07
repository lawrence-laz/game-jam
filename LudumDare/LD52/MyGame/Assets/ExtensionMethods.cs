using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods
{
    public static Interaction GetInvokableInteraction(this GameObject gameObject, Interactor interactor, GameObject target = null)
    {
        return gameObject
            .GetComponents<Interaction>()
            .FirstOrDefault(interaction => interaction.CanInvoke(interactor, target));
    }
}
