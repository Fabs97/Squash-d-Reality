using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
   
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float fireTime = .1f;
    void Start()
    {
        StartCoroutine("ShootingRoutine");

    }

    void Update() {
        // Shooting();
        // StartCoroutine("ShootingRoutine");

    }

    // void Shooting(){
    //     GameObject bullet = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
    //     Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //     rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    // }

    IEnumerator ShootingRoutine(){
        while(true){
            GameObject bullet = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            yield return new WaitForSeconds(fireTime*Time.deltaTime);
        } 
    }
}
