using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Shoot : MonoBehaviour
{
    //-----------------------OBJECTS-------------------------
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    //-------------------SHOOTING SETTINGS-------------------
    public float bulletForce = 2f;
    private float fireRatioTime = 2f;
    private float shootingTime = 5f;
    private float shootingDelay = 0.0f;
    private string bulletName;

    private bool canShoot = true; //TODO: true only for debugging, must be false
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetAxis("Fire")!=0 && canShoot)
        {
            StartBulletEmission();
        }
    }
    
    //-------------------------------------------------------------LOGIC FOR SHOOTING---------------------------------------------------------------
    private void StartBulletEmission()
    {
        canShoot = false;
        ShotgunBulletInstantiation();
        //BulletInstantiation("Pistol");
        StartCoroutine(fireRatio(fireRatioTime));
    }
    IEnumerator fireRatio(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
    
    void BulletInstantiation(string weaponType){
        shootingType(weaponType);
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position, this.transform.rotation);
        bullet.name = bulletName;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        Destroy(bullet,3f);
    }

    public void CanPlayerShoot(bool value)
    {
        canShoot = value;
    }

    public void ShotgunBulletInstantiation()
    {
        for (int i = 0; i < 4; i++)
        {
            float strayFactor = 10f;
            var randomNumberX = UnityEngine.Random.Range(-strayFactor, strayFactor);      
            var randomNumberY = UnityEngine.Random.Range(-strayFactor, strayFactor);     
            var randomNumberZ = UnityEngine.Random.Range(-strayFactor, strayFactor);  
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);   
            bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            bullet.name = "BulletShotgun";
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
            Destroy(bullet, 3f);  
        }
       

    }
    
    
     //------------------------------------------------------------SHOOTING SETTINGS------------------------------------------------------------------
     void shootingType(string weaponName)
     {
         if (weaponName == "Pistol")
         {
             bulletName = "BulletPistol";
         }else if (weaponName == "Shotgun")
         {
             bulletName = "BulletShotgun";
         }else if (weaponName == "AssaultRifle")
         {
             bulletName = "BulletAssaultRifle";
         }else if (weaponName == "SniperRifle")
         {
             bulletName = "BulletSniperRifle";
         }else if (weaponName == "SMG")
         {
             bulletName = "BulletSMG";
         }
         
     }
     //SET here shooting settings for different weapons
     void setShootingSettings(float bulletForce,float fireRatioTime, float shootingTime, float shootingDelay)
     {
         this.bulletForce = bulletForce;
         this.fireRatioTime = fireRatioTime;
         this.shootingTime = shootingTime;
         this.shootingDelay = shootingDelay;
     }
     
}
