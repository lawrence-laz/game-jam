using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LightningEffect : EffectBehaviour
{
    [SerializeField]
    private GameObject lightningStrike;
    [SerializeField]
    private GameObject burn;
    [SerializeField]
    private Image flash;

    public override IEnumerator PreShotEffect(Vector3 position)
    {
        yield return new WaitForEndOfFrame();
    }

    public override IEnumerator PostShotEffect(Vector3 position)
    {
        flash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        flash.enabled = false;
        lightningStrike.SetActive(true);
        SpriteRenderer sprite = lightningStrike.GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        lightningStrike.transform.position = position;
        Animator animator = lightningStrike.GetComponent<Animator>();
        animator.Play("LightningEffectAnimation");
        yield return animator.WhilePlaying();
        Instantiate(burn, position, transform.rotation, transform);
        sprite.enabled = false;
        lightningStrike.GetComponent<AudioSource>().pitch = 1 + Random.Range(-0.3f, 0.3f);
        do
        {
            yield return new WaitForEndOfFrame();
        } while (lightningStrike.GetComponent<AudioSource>().isPlaying);
        lightningStrike.SetActive(false);
    }
}
