using DG.Tweening;
using UnityEngine;

public class ExerciseVisualEffect : MonoBehaviour
{
    private void Start()
    {
        DOTween.Sequence()
            .Append(transform.DOLocalMoveY(0.1f, 0.5f))
            .AppendCallback(() =>
            {
                var face = transform.parent.GetComponentInChildren<Face>();
                if (face != null)
                {
                    face.SetFace(face.Intense);
                }
            })
            .Append(transform.DOLocalMoveY(0.15f, 1.5f))
            .Join(DOTween.Sequence()
                .Append(transform.DOLocalMoveX(0.02f, 0.05f))
                .Append(transform.DOLocalMoveX(-0.02f, 0.05f))
                .SetLoops(Mathf.FloorToInt(1.4f / 0.05f) / 2, LoopType.Yoyo)
                .SetRelative(true))
            .Append(transform.DOLocalMoveY(-0.25f, 0.5f))
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject)
            .SetRelative(true)
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
