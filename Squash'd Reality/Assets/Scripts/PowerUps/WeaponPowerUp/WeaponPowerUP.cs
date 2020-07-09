using System;
using UnityEngine;

public class WeaponPowerUP : PowerUP {
    protected Type weaponType;

    public override void triggerEnter(Collider other){
        if (other.tag == "Player")
        {
            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).tag.Equals("Weapon"))
                {
                    GameObject oldWeapon = other.transform.GetChild(i).gameObject;
                    Destroy(oldWeapon.GetComponent<Weapon>());
                    Weapon newWeapon = (Weapon) oldWeapon.AddComponent(weaponType);
                    oldWeapon.GetComponent<Shoot>().updateWeapon(newWeapon);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}