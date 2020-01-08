using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBehaviour : MonoBehaviour
{
    public GameObject Particles;
    public Collider2D ExplosionCollider;

    private SfxrSynth _explodeSound;

    private void Start()
    {
        _explodeSound = new SfxrSynth();
        _explodeSound.parameters.SetSettingsString("3,.5,,.2327,.5817,.398,.3,.1686,,-.0656,,,,,,,-.6488,.7027,,,,,,,,1,,,,,,");
    }

    public void Explode()
    {
        _explodeSound.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        ExplosionCollider.enabled = true;
        GetComponent<Animator>().StopPlayback();
        Particles.SetActive(true);
        Destroy(transform.parent.gameObject, 0.6f);
    }
}
