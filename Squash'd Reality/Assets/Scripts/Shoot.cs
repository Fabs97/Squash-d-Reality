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
    public float bulletForce;
    private float fireRatioTime;
    private float shootingTime;
    private string bulletName;
    private int numberOfBullets;
    private float spread;
    private bool canShoot = true; //TODO: true only for debugging, must be false

    void Start()
    {
        //TODO: settings per shotgun
        setShootingSettings(20f, 2f, "Shotgun", 4,10f);

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
        BulletInstantiation();
        StartCoroutine(fireRatio());
    }
    IEnumerator fireRatio()
    {
        yield return new WaitForSeconds(fireRatioTime);
        canShoot = true;
    }

    void BulletInstantiation(){
        for (int i = 0; i < numberOfBullets; i++)
        {
            var randomNumberX = UnityEngine.Random.Range(-spread, spread);      
            var randomNumberY = UnityEngine.Random.Range(-spread, spread);     
            var randomNumberZ = UnityEngine.Random.Range(-spread, spread);  
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);   
            bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
            Destroy(bullet, 3f);  
        }
    }

    public void CanPlayerShoot(bool value)
    {
        canShoot = value;
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
     void setShootingSettings(float bulletForce,float fireRatioTime, string weaponName, int bulletsNumber, float spread)
     {
         this.bulletForce = bulletForce;
         this.fireRatioTime = fireRatioTime;
         this.numberOfBullets = bulletsNumber;
         this.spread = spread;
         shootingType(name);
     }
     
}
