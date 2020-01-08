using DG.Tweening;
using Libs.Base.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    public float MaxHealth = 20;
    public UnityEvent OnGameOver;
    public AudioClip DeathClip;
    public GameObject DeathEffect;

    private float currentHealth;

    public AudioSource AudioSource { get; private set; }
    public Text Text { get; private set; }
    public CameraShake CameraShake { get; private set; }
    public SpriteRenderer DashBoard { get; private set; }

    private void Start()
    {
        Text = transform.Find("Healthmeter").GetComponentInChildren<Text>();
        CameraShake = FindObjectOfType<CameraShake>();
        DashBoard = transform.Find("Cabin/DashBoard").GetComponent<SpriteRenderer>();
        currentHealth = MaxHealth;
        AudioSource = transform.Find("HitSound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        var healthPercent = Mathf.Max(0, (int)(currentHealth / MaxHealth * 100));
        Text.text = healthPercent.ToString();
    }

    Sequence textAnimaion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.IsInLayerFrom(LayerMask.GetMask("EnemyBullet"))
            || collision.gameObject.name.Contains("Meteor"))
        {
            CameraShake.ScreenShake();

            DOTween.Sequence()
                .Append(DashBoard.DOColor(Color.red, 0.1f))
                .Append(DashBoard.DOColor(Color.white, 0.1f));
            currentHealth--;
            AudioSource.Play();

            if (currentHealth <= 5 && textAnimaion == null)
            {
                textAnimaion = DOTween.Sequence()
                    .Append(Text.DOColor(Color.red, 1))
                    .Append(Text.DOColor(Color.green, 1))
                    .SetLoops(-1);
            }

            if (currentHealth <= 0)
            {
                foreach (var enemy in FindObjectsOfType<EnemyGun>())
                {
                    enemy.enabled = false;
                }

                foreach (var sprite in transform.GetRoot().GetComponentsInChildren<SpriteRenderer>())
                {
                    sprite.enabled = false;
                }
                foreach (var text in transform.GetRoot().GetComponentsInChildren<Text>())
                {
                    text.enabled = false;
                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                AudioSource.PlayOneShot(DeathClip);
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                OnGameOver.Invoke();
                Destroy(this);
            }
        }
    }
}
