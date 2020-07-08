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
    public float bulletForce = 20f;
    private float fireRatioTime = 0.1f;
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
        BulletInstantiation("Pistol");
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
    
    //---------------------------------------------------LOGIC FOR SHOOTING WITHOUT CONTROL------------------------------------------------------------
    //BULLET instantiation in Shooting function 
    //CALL this to start shooting
    void ShootingUnstoppable()
    {
        InvokeRepeating("BulletInstantiation", shootingDelay, fireRatioTime);
        StartCoroutine("StopShooting");
    }
    //TIME to stop shooting
     IEnumerator StopShooting()
     {
         yield return new WaitForSeconds(shootingTime);
         CancelInvoke("BulletInstantiation");
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
