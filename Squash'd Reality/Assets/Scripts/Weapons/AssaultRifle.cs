using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    public override void Start(){
        base.Start();
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        bulletName = "BulletAssaultRifle";
        spread = 3f;
        fireRatioTime = 0.3f;
        magazine = 40;
        uiManager.setMagazineValue(magazine.ToString());
    }
}
