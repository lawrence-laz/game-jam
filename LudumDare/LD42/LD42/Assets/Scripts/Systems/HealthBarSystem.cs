using System.Collections.Generic;
using UnityEngine;

public class HealthBarSystem : MonoBehaviour
{
    private List<HealthComponent> _healths;

    private Texture2D _greenTexture;
    private Texture2D _redTexture;

    private Vector2 HealthBarSize = new Vector2(40, 10);
    private Vector2 HealthBarOffset = new Vector2(0, -30);

    private void OnEnable()
    {
        _healths = new List<HealthComponent>();
    }

    private void Start()
    {
        LoadTextures();
    }

    private void OnGUI()
    {
        if (Time.timeSinceLevelLoad < 1)
            return;

        if (GameOverSystem.Instance.GameOver || VictorySystem.Instance.Victory)
            return;

        DrawHealthBars();
    }

    internal void Register(HealthComponent healthComponent)
    {
        _healths.Add(healthComponent);
    }

    internal void Deregister(HealthComponent healthComponent)
    {
        _healths.Remove(healthComponent);
    }

    private void LoadTextures()
    {
        _greenTexture = new Texture2D(1, 1);
        _greenTexture.SetPixel(0, 0, Color.green);
        _greenTexture.Apply();

        _redTexture = new Texture2D(1, 1);
        _redTexture.SetPixel(0, 0, Color.red);
        _redTexture.Apply();
    }

    private void DrawHealthBars()
    {
        foreach (var health in _healths)
        {
            DrawHealthBars(health);
        }
    }

    private void DrawHealthBars(HealthComponent health)
    {
        // Red
        Vector2 screenPosition = (Vector2)Camera.main.WorldToScreenPoint(health.transform.position) + HealthBarOffset - Vector2.right * HealthBarSize.x / 2;
        screenPosition.y = Screen.height - screenPosition.y;
        Rect rect = new Rect(screenPosition, HealthBarSize);
        DrawQuad(rect, _redTexture);

        // Green
        float healthPercentage = health.Health / health.MaxHealth;
        Vector2 greenSize = HealthBarSize;
        greenSize.x *= healthPercentage;
        rect.size = greenSize;

        DrawQuad(rect, _greenTexture);
    }

    private void DrawQuad(Rect position, Texture2D texture)
    {
        GUI.DrawTexture(position, texture);
    }
}
