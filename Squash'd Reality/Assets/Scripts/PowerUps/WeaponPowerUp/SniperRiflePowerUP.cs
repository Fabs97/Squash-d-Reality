using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRiflePowerUP : WeaponPowerUP
{
    protected override void Start()
    {
        base.Start();
        base.weaponType = Type.GetType("SniperRifle");

    }
    
}
