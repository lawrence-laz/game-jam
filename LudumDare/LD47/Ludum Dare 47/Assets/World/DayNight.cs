using System;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    static class Day
    {
        public static Vector3 LightRotation => new Vector3(50, -30, 0);
        public static Color LightColor => Color.white;
        public static float LightIntensity => 1.35f;
    }

    static class Night
    {
        public static Vector3 LightRotation => new Vector3(50, -30, 0);
        public static Color LightColor => new Color(84f / 256, 92f / 256, 113f / 256);
        public static float LightIntensity => 1f;
    }

    public Light OutsideLight;
    public Light RoomLight;
    public GameObject Sleeping;

    public Clock Clock { get; private set; }
    public Camera Camera { get; private set; }

    private void Start()
    {
        Clock = FindObjectOfType<Clock>();
        Camera = Camera.main;
    }

    private void Update()
    {
        var daysProgress = (float)(Clock.Time.TotalMinutes / TimeSpan.FromHours(24).TotalMinutes);
        var lerp = (Mathf.Sin(daysProgress * 2 * Mathf.PI - Mathf.PI * 0.5f) + 1) / 2;

        OutsideLight.color = Color.Lerp(Night.LightColor, Day.LightColor, lerp);
        OutsideLight.intensity = Mathf.Lerp(Night.LightIntensity, Day.LightIntensity, lerp);

        RoomLight.enabled = Clock.IsDark && !Sleeping.activeInHierarchy;
    }
}
