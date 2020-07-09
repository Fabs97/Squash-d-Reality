using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGPowerUP : WeaponPowerUP
{
    private void Start() {
        base.weaponType = Type.GetType("SMG");
    }
}
