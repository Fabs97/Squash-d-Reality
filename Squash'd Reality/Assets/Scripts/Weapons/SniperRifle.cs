using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
{
    public override void Start(){
        base.Start();
        bulletName = "BulletSniperRifle";
    }
}
