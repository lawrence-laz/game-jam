using DG.Tweening;
using UnityEngine;

public class MaterialAnimation : MonoBehaviour
{
    public Material[] Materials;

    private void Start()
    {
        if (Materials == null || Materials.Length == 0)
        {
            return;
        }

        var renderer = GetComponent<MeshRenderer>();
        var sequence = DOTween.Sequence();

        foreach (var material in Materials)
        {
            sequence.AppendCallback(() => renderer.material = material);
            sequence.AppendInterval(Random.Range(0.1f, 0.5f));
        }

        sequence.SetLoops(-1);
        sequence.Play();
    }
}
