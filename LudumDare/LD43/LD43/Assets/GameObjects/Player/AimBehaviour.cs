using System;
using UnityEngine;
using UnityGoodies;

public class AimBehaviour : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ShootAimRaycast(1);
        }
        if (Input.GetMouseButtonDown(0))
        {
            ShootAimRaycast(0);
        }
    }

    private void ShootAimRaycast(int mouseButton)
    {
        var targets = Physics.RaycastAll(transform.position, transform.forward);

        foreach (var target in targets)
        {
            var targetObject = target.transform.gameObject;
            Debug.LogFormat("Aim targeted at {0}.", targetObject.name);

            var weapon = targetObject.transform.FindByTag("PlayersWeapon");
            if (weapon != null && TryPickup(weapon.gameObject))
            {
                return;
            }
        }

        foreach (var target in targets)
        {
            var targetObject = target.transform.gameObject;
            Debug.LogFormat("Aim targeted at {0}.", targetObject.name);

            if (mouseButton == 1
                && targetObject.layer == LayerMask.NameToLayer("Enemy")
                && TryStab(target.point, targetObject.transform))
            {
                Debug.LogFormat("{0} was stabbed by {1}", targetObject.name, gameObject.name);
                return;
            }

            if (mouseButton == 0
                && TryCast(target.point, targetObject.transform))
            {
                Debug.LogFormat("{1} casted a magic bolt towards {0}", targetObject.name, gameObject.name);
                return;
            }
        }

        if (mouseButton == 1)
        {
            TryStab(Camera.main.transform.forward * 100, null);
            return;
        }
    }

    private bool TryCast(Vector3 point, Transform transform)
    {
        return this.ForAllComponentsInRootsChildren<BoltShooterBehaviour>(
            x => x.Shoot(point)
        );
    }

    private bool TryPickup(GameObject targetObject)
    {
        Debug.LogFormat("Trying to pick up {0}", targetObject.name);
        var pickup = transform.GetRoot().GetComponentInChildren<WeaponPickUpBehaviour>();
        if (pickup == null || pickup.transform.childCount > 0)
        {
            return false;
        }

        pickup.PickUp(targetObject);
        return true;
    }

    private bool TryStab(Vector3 point, Transform transform)
    {
        return this.ForAllComponentsInRootsChildren<StabBehaviour>(
            x => x.Stab(point, transform)
        );
    }
}
