using UnityEngine;
using UnityEngine.Networking;

public class Shoot : MonoBehaviour
{
    //-------------------SHOOTING SETTINGS-------------------

    private Weapon weapon;

    void Start()
    {
        weapon = gameObject.GetComponent<Weapon>();
    }

    private void Update()
    {
        if (Input.GetAxis("Fire")!=0 && weapon.canShoot)
        {
            // StartBulletEmission();
            weapon.shoot();
        }
    }
    
   
}
