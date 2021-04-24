using System;
using UnityEngine;

public class EngineFX : MonoBehaviour
{
    public ParticleSystem TopLeftEngine;
    public ParticleSystem TopRightEngine;
    public ParticleSystem BotLeftEngine;
    public ParticleSystem BotRightEngine;
    public float EmissionRate;

    private Navigation _navigation;

    private void Start()
    {
        _navigation = GetComponent<Navigation>();
        transform.parent.GetComponent<Health>().OnDead.AddListener(OnDead);
    }

    private void OnDead()
    {
        var emission = BotLeftEngine.emission;
        emission.rateOverTime = 0;

        emission = BotRightEngine.emission;
        emission.rateOverTime = 0;

        emission = TopLeftEngine.emission;
        emission.rateOverTime = 0;

        emission = TopRightEngine.emission;
        emission.rateOverTime = 0;

        enabled = false;
    }

    private void Update()
    {
        UpdateParticleFx();
    }

    private void UpdateParticleFx()
    {
        var emission = BotLeftEngine.emission;
        emission.rateOverTime = EmissionRate * _navigation.BotLeftEnginePower;

        emission = BotRightEngine.emission;
        emission.rateOverTime = EmissionRate * _navigation.BotRightEnginePower;

        emission = TopLeftEngine.emission;
        emission.rateOverTime = EmissionRate * _navigation.TopEnginesPower;

        emission = TopRightEngine.emission;
        emission.rateOverTime = EmissionRate * _navigation.TopEnginesPower;
    }
}
