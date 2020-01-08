using System.Collections;
using UnityEngine;

public class ZombieEffect : EffectBehaviour
{
    [SerializeField]
    private GameObject greenExplosion;

    public override IEnumerator PreShotEffect(Vector3 position)
    {
        yield return new WaitForEndOfFrame();
    }

    public override IEnumerator PostShotEffect(Vector3 position)
    {
        greenExplosion.SetActive(true);
        greenExplosion.transform.position = position;
        Animator animator = greenExplosion.GetComponent<Animator>();
        animator.Play("GreenExplosion");
        yield return animator.WhilePlaying();
        greenExplosion.SetActive(false);
    }
}