using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterSoundController : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }
    public ShipControls ShipControls { get; private set; }

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        ShipControls = GetComponentInParent<ShipControls>();
    }

    private void Update()
    {
        var thurstersOn = Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.E)
            || Input.GetKey(KeyCode.Space);

        if (!thurstersOn)
        {
            if (AudioSource.isPlaying)
            {
                if (AudioSource.pitch != 0)
                {
                    AudioSource.pitch = Mathf.MoveTowards(AudioSource.pitch, 0, 10 * Time.deltaTime);
                }
                else
                {
                    AudioSource.Stop();
                }
            }

            return;
        }

        if (!AudioSource.isPlaying)
            AudioSource.Play();

        AudioSource.pitch = Mathf.Min(3, ShipControls.Velocity.sqrMagnitude / 16 * 3);
    }
}
