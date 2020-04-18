using Libs.Base.GameLogic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Invoke()
    {
        Debug.Log("OUch"); // TODO:
        FindObjectOfType<SceneLoadingBehaviour>().RestartScene();
    }
}
