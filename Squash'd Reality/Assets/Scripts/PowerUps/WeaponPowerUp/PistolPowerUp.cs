using System;
using UnityEngine;

public class PistolPowerUp : WeaponPowerUP {
    private void Start() {
        base.weaponType = Type.GetType("Pistol");
    }
}