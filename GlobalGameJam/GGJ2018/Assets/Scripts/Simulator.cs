using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    private const float FluctuationSpeed = 0.1f;

    public float NaturalVelocity = 0;
    public float MemeVelocity = 0;
    public float MemeVelocityFadeOutDuration = 0.1f;
    public float Threshold = 0.4f;
    public float ChangeSpeed = 5;

    private Coin coin;
    private Vector2 position;
    private Vector2 direction;

    private void OnEnable()
    {
        coin = GetComponent<Coin>();
        position = new Vector2(Random.Range(0, 1000), Random.Range(0, 1000));
        direction = Random.insideUnitCircle.normalized;
        CancelInvoke();
        InvokeRepeating("SimulationStep", 2, 2);
    }

    private void Update()
    {
        MemeVelocity = Mathf.MoveTowards(MemeVelocity, 0, Mathf.Abs(MemeVelocity) * Time.deltaTime / MemeVelocityFadeOutDuration);

        position += direction * FluctuationSpeed * Time.deltaTime;
        NaturalVelocity = Mathf.PerlinNoise(position.x, position.y) - Threshold;
        if(coin.Value <= 0)
        {
            coin.enabled = false;
            enabled = false;
            CancelInvoke();
        }
    }

    private void SimulationStep()
    {
        if (HardwareManager.IsInWinCondition)
        {
            coin.Value -= coin.IsMining ? 10 : 3f;

            if (coin.Value < 0)
                coin.Value = 0;
            return;
        }

        coin.Value += NaturalVelocity * ChangeSpeed + MemeVelocity;
        if (coin.Value < 0)
            coin.Value = 0;
    }
}
