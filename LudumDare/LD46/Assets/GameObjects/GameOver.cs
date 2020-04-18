using Libs.Base.GameLogic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("OUch"); // TODO:
        FindObjectOfType<SceneLoadingBehaviour>().RestartScene();
    }
}
