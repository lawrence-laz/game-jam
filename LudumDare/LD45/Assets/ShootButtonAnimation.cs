using Libs.Base.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButtonAnimation : SpriteChange
{
    public ShipGun Gun { get; private set; }

    protected override void Start()
    {
        base.Start();
        Gun = transform.GetRoot().GetComponentInChildren<ShipGun>();
    }

    private void Update()
    {
        if (Gun.IsShooting)
        {
            SetSprite1();
        }
        else
        {
            SetSprite2();
        }
    }
}
