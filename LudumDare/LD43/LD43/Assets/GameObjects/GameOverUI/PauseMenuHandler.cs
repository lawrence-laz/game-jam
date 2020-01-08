using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGoodies;

public class PauseMenuHandler : MonoBehaviour
{
    private bool _isPaused;
    private float _timeScale;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                gameObject.EnableComponentsInChildren(false,
                    typeof(Canvas),
                    typeof(GraphicRaycaster));
                Time.timeScale = _timeScale;
                CursorHelper.Instance.ShowCursor = false;
            }
            else
            {
                _timeScale = Time.timeScale;
                Time.timeScale = 0;
                gameObject.EnableComponentsInChildren(true,
                    typeof(Canvas),
                    typeof(GraphicRaycaster));
                CursorHelper.Instance.ShowCursor = true;
            }

            _isPaused = !_isPaused;
        }
    }
}
