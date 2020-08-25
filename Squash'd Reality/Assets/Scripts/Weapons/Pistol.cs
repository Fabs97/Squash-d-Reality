using UnityEngine;

public class Pistol : Weapon {
    public override void Start(){
        base.Start();
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        bulletName = "BulletPistol";
        spread = 0.2f;
        fireRatioTime = 0.55f;
        magazine = 999;
        uiManager.setMagazineValue(magazine.ToString());

    }
}