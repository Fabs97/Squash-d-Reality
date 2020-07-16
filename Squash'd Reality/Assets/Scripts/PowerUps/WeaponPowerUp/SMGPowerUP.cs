using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGPowerUP : WeaponPowerUP
{
    protected override void Start()
    {
        base.Start();
        base.weaponType = Type.GetType("SMG");

    }
    
}
