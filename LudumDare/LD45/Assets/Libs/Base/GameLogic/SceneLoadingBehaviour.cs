using DG.Tweening;
using Libs.Base.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Libs.Base.GameLogic
{
    public class SceneLoadingBehaviour : MonoBehaviour
    {
        [SerializeField] string sceneName;

        [ContextMenu("LoadScene")]
        public void LoadScene()
        {
            if (!string.IsNullOrWhiteSpace(sceneName))
            {
                LoadScene(sceneName);
            }
            else
            {

                if (load != null)
                    return;

                ScreenFade.Instance.FadeOut();
                load = DOTween.Sequence()
                    .AppendInterval(1)
                    .AppendCallback(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
            }
        }

        [ContextMenu("LoadSceneAsync")]
        public void LoadSceneAsync()
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        Sequence load;

        public void LoadScene(string name)
        {
            if (load != null)
                return;

            ScreenFade.Instance.FadeOut();
            load = DOTween.Sequence()
                .AppendInterval(1)
                .AppendCallback(() => SceneManager.LoadScene(name));
        }
    }
}
