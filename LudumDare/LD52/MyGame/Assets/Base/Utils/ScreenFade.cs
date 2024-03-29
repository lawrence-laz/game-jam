﻿using System.Collections;
using UnityEngine;

public class ScreenFade : Singleton<ScreenFade>
{
    [SerializeField]
    private Color fadeColor = Color.black;
    [SerializeField] float defaultDuration = 1;
    public bool AutoFadeIn = true;

    private Texture2D fadeTexture;
    private float startTime;
    private float duration;
    private bool isFading = false;

    private void Start()
    {
        if (AutoFadeIn)
        {
            FadeIn(defaultDuration);
        }
        else
        {
            FadeIn(0);
        }
    }

    public void FadeOut()
    {
        FadeOut(defaultDuration);
    }

    public void FadeOut(float duration)
    {
#if UNITY_EDITOR
        if (fadeTexture.GetPixel(0, 0) != fadeColor)
            fadeTexture = GetTextureByColor(fadeColor);
#endif
        this.duration = duration;
        StopAllCoroutines();
        StartCoroutine(FadeAlpha(0, 1));
    }

    [ContextMenu("Flash")]
    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        isFading = true;
        Color fromColor = fadeColor;
        fromColor.a = 0;
        fadeTexture.SetPixel(0, 0, fromColor);
        fadeTexture.Apply();
        startTime = Time.time;

        yield return new WaitForEndOfFrame();

        while (Time.time - startTime < 0.04f)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(0, 1, (Time.time - startTime) / duration);
            fadeTexture.SetPixel(0, 0, newColor);
            fadeTexture.Apply();

            yield return new WaitForEndOfFrame();
        }

        Color toColor = fadeColor;
        toColor.a = 0;
        fadeTexture.SetPixel(0, 0, toColor);
        fadeTexture.Apply();

        fromColor = fadeColor;
        fromColor.a = 1;
        fadeTexture.SetPixel(0, 0, fromColor);
        fadeTexture.Apply();
        startTime = Time.time;

        yield return new WaitForEndOfFrame();

        while (Time.time - startTime < 0.04f)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(1, 0, (Time.time - startTime) / duration);
            fadeTexture.SetPixel(0, 0, newColor);
            fadeTexture.Apply();

            yield return new WaitForEndOfFrame();
        }

        toColor = fadeColor;
        toColor.a = 0;
        fadeTexture.SetPixel(0, 0, toColor);
        fadeTexture.Apply();

        isFading = false;
        yield break;
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        this.duration = duration;
        yield return StartCoroutine(FadeAlpha(0, 1));
    }

    public void FadeIn(float duration)
    {
        if (fadeTexture == null)
        {
            fadeTexture = GetTextureByColor(fadeColor);
        }

#if UNITY_EDITOR
        if (fadeTexture.GetPixel(0, 0) != fadeColor)
            fadeTexture = GetTextureByColor(fadeColor);
#endif
        this.duration = duration;
        StopAllCoroutines();
        StartCoroutine(FadeAlpha(1, 0));
    }

    public void Test(float a, float b)
    {

    }

    private void OnEnable()
    {
        if (AutoFadeIn)
        {
            fadeTexture = GetTextureByColor(fadeColor);
        }
    }

    private void OnGUI()
    {
        if (fadeTexture == null)
        {
            return;
        }

        if (isFading || fadeTexture.GetPixel(0, 0).a != 0)
        {
            GUI.DrawTexture(new Rect(Vector2.zero, new Vector2(Screen.width, Screen.height)), fadeTexture);
        }
    }

    private IEnumerator FadeAlpha(float from, float to)
    {
        isFading = true;
        Color fromColor = fadeColor;
        fromColor.a = from;
        fadeTexture.SetPixel(0, 0, fromColor);
        fadeTexture.Apply();
        startTime = Time.time;

        yield return new WaitForEndOfFrame();

        while (Time.time - startTime < duration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(from, to, (Time.time - startTime) / duration);
            fadeTexture.SetPixel(0, 0, newColor);
            fadeTexture.Apply();

            yield return new WaitForEndOfFrame();
        }

        Color toColor = fadeColor;
        toColor.a = to;
        fadeTexture.SetPixel(0, 0, toColor);
        fadeTexture.Apply();

        isFading = false;
        yield break;
    }

    private Texture2D GetTextureByColor(Color color)
    {
        Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        texture.SetPixel(0, 0, color);
        texture.Apply();

        return texture;
    }
}
