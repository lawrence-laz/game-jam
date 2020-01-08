using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationBehaviour : MonoBehaviour
{
    public GameObject SimpleSprite;
    public GameObject DetonatedSprite;

    private HealthComponent _health;

    private void Start()
    {
        _health = GetComponent<HealthComponent>();
    }

    private void Update()
    {
        if (_health.Health != 100)
            Detonate();
    }

    private void Detonate()
    {
        SimpleSprite.SetActive(false);
        DetonatedSprite.SetActive(true);
        Destroy(this);
    }
}
