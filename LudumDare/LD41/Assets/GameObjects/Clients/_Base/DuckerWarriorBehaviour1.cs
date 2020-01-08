using DreamBit.Extensions;
using System.Collections;
using UnityEngine;

public class DuckerWarriorBehaviour1 : ClientBehaviour
{
    Coroutine ai;

    protected override void Think()
    {
        this.StartCoroutineIfNotStarted(ref ai, AiCoroutine());
    }

    private IEnumerator AiCoroutine()
    {
        while (!IsInTargetPosition)
            yield return null;

        Speech.Instance.Say("Hello!");
        yield return new WaitForSeconds(3);
        Leave();

        ai = null;
    }
}
