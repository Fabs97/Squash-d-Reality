using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPowerUP : WeaponPowerUP
{
    protected override void Start()
    {
        base.Start();
        base.weaponType = Type.GetType("Shotgun");

    }
    
}
