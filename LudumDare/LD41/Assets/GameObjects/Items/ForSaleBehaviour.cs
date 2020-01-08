using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSaleBehaviour : InteractableBehaviour
{
    public int Price { get; internal set; }

    new Sequence animation;

    private void Start()
    {
        Vector3 scale = transform.localScale;

        animation = DOTween.Sequence()
            .Append(transform.DOScale(scale * 1.1f, 0.5f))
            .Append(transform.DOScale(scale, 0.5f))
            .SetAutoKill(false)
            .OnComplete(() => animation.Restart())
            .Play();
    }

    private void StopAnimation()
    {
        animation.OnComplete(null);
        animation.SetAutoKill(true);
    }

    public override bool Interact(Interactor interactor)
    {
        if (Chest.Instance.Gold >= Price)
        {
            Chest.Instance.Gold -= Price;
            StopAnimation();
            Destroy(this);
        }
        else
        {
            Vector3 rotation = Camera.main.transform.rotation.eulerAngles;
            Camera.main.GetComponent<MouseLook>().enabled = false;

            Sequence s = DOTween.Sequence()
                .Append(Camera.main.transform.DOLookAt(FindObjectOfType<Chest>().transform.position, 1))
                .Append(Camera.main.transform.DORotate(rotation, 1)
                .OnComplete(() => Camera.main.GetComponent<MouseLook>().enabled = true))
                .Play();
        }

        return base.Interact(interactor);
    }
}
