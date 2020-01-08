using UnityEngine;

public class PauseManager : MonoBehaviour
{
    HeroStats _stats;
    bool _paused;

    private void OnEnable()
    {
        _stats = HeroStats.Get();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
            && _stats.Health > 0 
            && Time.timeScale != 0)
        {
            _paused = true;
            Pause();
        }

        else if (_paused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            _paused = false;
            Unpause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        transform.Find("Pause_Object").GetComponent<SlideInOut>().SlideIn();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        transform.Find("Pause_Object").GetComponent<SlideInOut>().SlideOut();
    }
}
