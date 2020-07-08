using System.Collections;
using UnityEngine;

public class Shotgun : Weapon {
    [Range(0, 10)][SerializeField] private int numberOfBullets = 4;
    public override void Start(){
        base.Start();
    }
    
    public override void shoot(){
        BulletInstantiation();
        StartCoroutine(fireRatio());
    }
    IEnumerator fireRatio()
    {
        yield return new WaitForSeconds(fireRatioTime);
    }

    void BulletInstantiation(){
        for (int i = 0; i < numberOfBullets; i++)
        {
            var randomNumberX = UnityEngine.Random.Range(-spread, spread);      
            var randomNumberY = UnityEngine.Random.Range(-spread, spread);     
            var randomNumberZ = UnityEngine.Random.Range(-spread, spread);  
            var bullet = Instantiate(base.bulletPrefab, base._firePoint.position, base._firePoint.rotation);   
            bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
            Destroy(bullet, 3f);  
        }
    }
}