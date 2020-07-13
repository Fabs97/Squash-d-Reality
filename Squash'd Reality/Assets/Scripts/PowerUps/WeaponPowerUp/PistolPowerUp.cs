using System;
using UnityEngine;

public class PistolPowerUp : WeaponPowerUP {
    protected override void Start()
    {
        base.Start();
        base.weaponType = Type.GetType("Pistol");

    }

}