using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRiflePowerUP : WeaponPowerUP
{
    private void Start() {
        base.weaponType = Type.GetType("AssaultRifle");
    }
}
