using DG.Tweening;
using UnityEngine;

public class GamingVisualEffects : MonoBehaviour, IVisualEffect
{
    private void Start()
    {

        DOTween.Sequence()
            .Append(transform.DOShakePosition(3f, 0.01f, 10, 100, fadeOut: false))
            .Append(transform.DOLocalMove(new Vector3(0.047f, 0.078f, -0.11f), 0.3f))
            .Join(transform.DOLocalRotate(new Vector3(0, 0, 28.635f), 0.3f))
            .AppendCallback(() =>
            {
                var face = transform.parent.GetComponentInChildren<Face>();
                if (face != null)
                {
                    face.SetFace(face.VeryExcided);
                }
            })
            .Append(transform.DOShakePosition(10f, 0.1f, 20, 100, fadeOut: false))
            .Play();
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
