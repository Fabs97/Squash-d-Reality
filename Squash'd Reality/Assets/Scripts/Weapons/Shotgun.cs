using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : Weapon {
    public override void Start(){
        base.Start();
        numberOfBullets = 4;
        bulletName = "BulletShotgun";

    }
    
    
}