using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float Health = 100;
    public float MaxHealth = 100;
    public bool IsOutOfBounds;

    public event Action<WeaponComponent, HealthComponent> OnDamagedByWeapon;

    private void Start()
    {
        FindObjectOfType<HealthBarSystem>().Register(this);
        OnDamagedByWeapon += FindObjectOfType<WeaponDamageSystem>().HandleOnDamagedByWeapon;
    }

    private void OnDestroy()
    {
        HealthBarSystem hp = FindObjectOfType<HealthBarSystem>();
        if (hp != null)
            hp.Deregister(this);
        var weaponDamageSystem = FindObjectOfType<WeaponDamageSystem>();
        if (weaponDamageSystem)
            OnDamagedByWeapon -= weaponDamageSystem.HandleOnDamagedByWeapon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform parent = other.transform.parent;
        Transform parentParent = null;
        if (parent != null)
            parentParent = parent.parent;

        if (other.tag != "Weapon")
            return;

        if (other.GetComponentInParent<ProjectileComponent>() != null && tag == "Enemy")
            return;

        if (tag == "Enemy" && parentParent != null && parentParent.tag == "Enemy")
            return;

        if (other.GetComponentInParent<HealthComponent>() == this)
            return;

        if (tag == "Bomb" && parentParent != null && parentParent.tag == "Enemy")
            return;

        if (parentParent != null && parentParent.tag == "Bomb" && tag == "Player")
            return;

        if (OnDamagedByWeapon != null)
            OnDamagedByWeapon.Invoke(other.GetComponentInParent<WeaponComponent>(), this);
    }
}
