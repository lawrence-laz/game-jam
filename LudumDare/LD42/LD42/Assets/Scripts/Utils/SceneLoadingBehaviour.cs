using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingBehaviour : MonoBehaviour
{
    [SerializeField] string sceneName;

    Coroutine coroutine = null;

    public string SceneName { get { return sceneName; } }

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
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void RestartScene()
    {
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

        SceneManager.LoadScene(name);
    }

    private IEnumerator LoadScene(int buildIndex, float delay)
    {
        //Debug.Log($"Loadng scene index: {buildIndex}");
        yield return ScreenFade.Instance.FadeOutCoroutine(delay);

        SceneManager.LoadScene(buildIndex);
    }
}
