using Libs.Base.GameLogic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public SceneLoadingBehaviour SceneLoader { get; set; }

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        SceneLoader = GetComponent<SceneLoadingBehaviour>();
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        if (steppedBy.Any(x => x.tag == "Player"))
        {
            if (steppedBy.TryGet<Hero>(out var hero))
            {
                if (FindObjectsOfType<Hostage>().All(x => hero.Hostages.Contains(x)))
                {
                    Debug.Log("Next level"); // TODO
                    FindObjectOfType<TurnManager>().enabled = false;
                    SceneLoader.LoadScene();
                }
                else 
                {
                    Debug.Log("Save your friends"); // TODO
                }
            }

        }
    }
}
