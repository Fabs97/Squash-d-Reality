using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Shotgun : Weapon {
    [Range(0, 10)][SerializeField] private int numberOfBullets = 4;
    public override void Start(){
        base.Start();
    }
    
    public override void shoot()
    {
        if (GetComponentInParent<DummyMoveset>().hasAuthority)
        {
            canShoot = false;
            BulletInstantiation();
            StartCoroutine(fireRatio());   
        }
      
    }
    IEnumerator fireRatio()
    {
        yield return new WaitForSeconds(fireRatioTime);
        this.canShoot = true;
    }

    void BulletInstantiation(){
        for (int i = 0; i < numberOfBullets; i++)
        {
          
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSpawnBullets(base._firePoint.position, base._firePoint.rotation, spread, bulletForce);
            
        }
    }
}