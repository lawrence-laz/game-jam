using UnityEngine;

public class CursorHelper : Singleton<CursorHelper>
{
    public bool ShowCursor = false;

    public void SetShowCursor(bool show)
    {
        ShowCursor = show;
    }

    private void Update()
    {
        Cursor.lockState = ShowCursor ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = ShowCursor;
    }
}
