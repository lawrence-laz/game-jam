using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Gun _gun;
    private Navigation _navigation;
    private Lander _lander;

    private void Start()
    {
        _gun = GetComponentInChildren<Gun>();
        _navigation = GetComponentInChildren<Navigation>();
        _lander = GetComponentInChildren<Lander>();
    }

    private void Update()
    {
        HandleGun();
        HandleNavigation();
    }

    private void HandleNavigation()
    {
        _navigation.Rotate(Input.GetAxis("Horizontal"));
        _navigation.Move(Input.GetAxis("Vertical"));
    }

    private void HandleGun()
    {
        if (_lander.IsLanded && _lander.Target != null && _lander.Target.GetComponentInParent<SpaceStation>() != null)
        {
            return;
        }

        if (_gun == null)
        {
            _gun = GetComponentInChildren<Gun>();

            if (_gun == null)
            {
                return;
            }
        }

        _gun.transform.LookAt2D(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _gun.IsShooting = Input.GetMouseButton(0);
    }
}
