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
        Debug.Log("CAN SHOOT: " + weapon.canShoot);
        if (Input.GetAxis("Fire")!=0 && weapon.canShoot)
        {
            // StartBulletEmission();
            weapon.shoot();
        }
    }

    public void updateWeapon(Weapon weaponToUpdate)
    {
        weapon = weaponToUpdate;
        Debug.Log("WEAPON: " + weapon);
        weapon.canShoot = true;
    }
    
   
}
