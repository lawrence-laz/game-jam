using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public ScreenFade ScreenFlashEffect;
    public GameObject FlamePrefab;

    private Sequence _animation;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        ScreenFlashEffect = Camera.main.GetComponentInChildren<ScreenFade>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // _spriteRenderer.enabled = false;
        Strike();
    }

    [ContextMenu("Strike")]
    public void Strike()
    {
        var target = FindObjectsOfType<Label>().FirstOrDefault(label => label.Is("dirt"))
            ?? FindObjectsOfType<Label>().FirstOrDefault(label => label.Is("wheat"))
            ?? FindObjectsOfType<Label>().FirstOrDefault(label => label.Is("sand"))
            ?? FindObjectsOfType<Label>().FirstOrDefault(label => label.Is("hole"))
            ?? FindObjectsOfType<Label>().FirstOrDefault(label => label.Is("chest"));

        transform.position = target.transform.position;
        Destroy(target.gameObject);
        var flame = Instantiate(FlamePrefab);
        flame.transform.position = transform.position;

        _animation?.Kill();
        _animation = DOTween.Sequence()
            .AppendCallback(() =>
            {
                _spriteRenderer.enabled = true;
                var color = _spriteRenderer.color;
                color.a = 1;
                _spriteRenderer.color = color;
                ScreenFlashEffect.Flash();
            })
            .Append(_spriteRenderer.DOFade(0, 0.7f))
            .AppendCallback(() =>
            {
                _spriteRenderer.enabled = false;
                Destroy(gameObject, 0.1f);
            })
            .Play();
    }
}
