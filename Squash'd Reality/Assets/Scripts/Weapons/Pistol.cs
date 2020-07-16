using UnityEngine;

public class Pistol : Weapon {
    public override void Start(){
        base.Start();
        bulletName = "BulletPistol";
        spread = 0.2f;
        fireRatioTime = 0.5f;

    }
}