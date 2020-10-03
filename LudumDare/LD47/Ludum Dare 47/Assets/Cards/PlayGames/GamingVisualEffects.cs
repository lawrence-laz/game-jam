using UnityEngine;

public class GamingVisualEffects : MonoBehaviour, IVisualEffect
{
    private void Start()
    {
        var face = transform.parent.GetComponentInChildren<Face>();
        if (face != null)
        {
            face.SetFace(face.VeryExcided);
        }
    }

    private void OnDestroy()
    {
        var face = transform.parent.GetComponentInChildren<Face>();
        if (face != null)
        {
            face.ResetFace();
        }
    }
}
