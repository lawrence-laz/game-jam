using UnityEngine;

class TutorialPanelBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            panel.SetActive(true);
            PlayerPrefs.SetInt("FirstTime", 1);
        }
    }
}
