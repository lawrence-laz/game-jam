using UnityEngine;

public class Action : Singleton<Action>
{
    public bool action = false;

    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
            action = false;
        else if (Input.GetButtonDown("Fire1"))
            action = true;
    }
}
