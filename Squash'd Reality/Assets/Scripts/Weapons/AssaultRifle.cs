using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    public override void Start(){
        base.Start();
        bulletName = "BulletAssaultRifle";
        spread = 3f;
        fireRatioTime = 0.6f;
    }
}
