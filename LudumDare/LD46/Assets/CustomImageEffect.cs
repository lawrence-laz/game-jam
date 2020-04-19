using UnityEngine;

[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour
{
    public Material Effect;
    [Range(0, 1)] public float Force = 0.0f;
    [Range(0, 1)] public float Force2 = 0.0f;
    [Range(-3, 20)] public float Contrast = 0.0f;
    [Range(-200, 200)] public float Brightness = 0.0f;
    [Range(0, 1)] public float ScanColor;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Effect.SetFloat("_VertsColor", 1 - Force);
        Effect.SetFloat("_VertsColor2", 1 - Force2);
        Effect.SetFloat("_Contrast", Contrast);
        Effect.SetFloat("_Br", Brightness);
        Effect.SetFloat("_ScansColor", ScanColor);
        Graphics.Blit(source, destination, Effect);
    }
}
