using System.Collections;
using UnityEngine;

public class EffectBehaviour : MonoBehaviour
{
    public virtual IEnumerator PreShotEffect(Vector3 position)
    {
        return null;
    }

    public virtual IEnumerator PostShotEffect(Vector3 position)
    {
        return null;
    }
}
