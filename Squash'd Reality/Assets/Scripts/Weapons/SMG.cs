using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    public override void Start(){
        base.Start();
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        bulletName = "BulletSMG";
        spread = 25f;
        fireRatioTime = 0.1f;
        magazine = 50;
        uiManager.setMagazineValue(magazine.ToString());

    }
}
