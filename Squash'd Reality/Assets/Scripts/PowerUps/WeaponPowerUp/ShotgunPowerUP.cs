using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPowerUP : WeaponPowerUP
{
    private void Start() {
        base.weaponType = Type.GetType("Shotgun");
    }
}
