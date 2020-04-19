using Libs.Base.GameLogic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public TileObject TileObject { get; set; }
    public SceneLoadingBehaviour SceneLoader { get; set; }

    public UnityEvent OnOpenned;

    private void Start()
    {
        TileObject = GetComponent<TileObject>();
        TileObject.OnStepped.AddListener(OnStepped);
        SceneLoader = GetComponent<SceneLoadingBehaviour>();
    }

    private void OnStepped(GameObject[] steppedBy)
    {
        if (steppedBy.TryGet<Hero>(out var hero))
        {
            foreach (var hostage in hero.Hostages)
            {
                hostage.enabled = false;
                Destroy(hostage.gameObject); // TODO: nice effect.
            }
            hero.Hostages.Clear();

            if (!FindObjectsOfType<Hostage>().Where(x => x.enabled).Any())
            {
                OnOpenned.Invoke();
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
