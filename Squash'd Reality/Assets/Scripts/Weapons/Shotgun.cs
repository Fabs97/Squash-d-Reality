using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : Weapon {
    public override void Start(){
        base.Start();
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        numberOfBullets = 4;
        bulletName = "BulletShotgun";
        fireRatioTime = 1.5f;
        magazine = 10;
        spread = 5f;
        uiManager.setMagazineValue(magazine.ToString());
    }
    
    
}