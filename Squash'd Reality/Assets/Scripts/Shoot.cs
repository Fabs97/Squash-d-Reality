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
    void Start()
    {
        
    }

    //CALL this to start shooting
    void Shooting()
    {
        InvokeRepeating("BulletInstantiation", shootingDelay, fireRatioTime);
        StartCoroutine("StopShooting");
    }
    
    //BULLET instantiation in Shooting function 
    void BulletInstantiation(){
         GameObject bullet = Instantiate(bulletPrefab,firePoint.position, bulletPrefab.transform.rotation);
         Rigidbody rb = bullet.GetComponent<Rigidbody>();
         rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
     }
     
    //TIME to stop shooting
     IEnumerator StopShooting()
     {
         yield return new WaitForSeconds(shootingTime);
         CancelInvoke("BulletInstantiation");
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
