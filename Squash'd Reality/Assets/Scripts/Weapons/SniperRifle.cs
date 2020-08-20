using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
{
    public override void Start(){
        base.Start();
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        bulletName = "BulletSniperRifle";
        spread = 0f;
        fireRatioTime = 1.2f;
        magazine = 5;
        uiManager.setMagazineValue(magazine.ToString());
    }
}
