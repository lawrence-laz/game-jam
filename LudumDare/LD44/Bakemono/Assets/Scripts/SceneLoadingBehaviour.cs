using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingBehaviour : MonoBehaviour
{
    [SerializeField] string sceneName;

    Coroutine coroutine = null;

    [ContextMenu("LoadScene")]
    public void LoadScene()
    {
        LoadScene(sceneName);
    }

    public void LoadScene(float fadeOutDuration)
    {
        if (coroutine != null)
            return;

        coroutine = StartCoroutine(LoadScene(sceneName, fadeOutDuration));
    }

    [ContextMenu("LoadSceneAsync")]
    public void LoadSceneAsync()
    {
        DOTween.Clear(true);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadScene(string name)
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(name);
    }

    public void RestartScene()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartScene(float fadeOutDuration)
    {
        if (coroutine != null)
            return;

        coroutine = StartCoroutine(LoadScene(SceneManager.GetActiveScene().name, fadeOutDuration));
    }

    public void LoadNextScene()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextScene(float fadeOutDuration)
    {
        if (coroutine != null)
            return;

        coroutine = StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1, fadeOutDuration));
    }

    private IEnumerator LoadScene(string name, float delay)
    {
        //Debug.Log($"Loadng scene: {name}");
        yield return ScreenFade.Instance.FadeOutCoroutine(delay);

        DOTween.Clear(true);
        SceneManager.LoadScene(name);
    }

    private IEnumerator LoadScene(int buildIndex, float delay)
    {
        //Debug.Log($"Loadng scene index: {buildIndex}");
        yield return ScreenFade.Instance.FadeOutCoroutine(delay);

        DOTween.Clear(true);
        SceneManager.LoadScene(buildIndex);
    }
}