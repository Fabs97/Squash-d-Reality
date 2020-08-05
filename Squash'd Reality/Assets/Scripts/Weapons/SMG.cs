using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    public override void Start(){
        base.Start();
        bulletName = "BulletSMG";
        spread = 25f;
        fireRatioTime = 0.4f;
        magazine = 50;

    }
}
