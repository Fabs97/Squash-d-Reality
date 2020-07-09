using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    private GameObject weapon;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            for (int i = 0; i < other.transform.childCount; i++)
            {
                if (other.transform.GetChild(i).tag.Equals("Weapon"))
                {
                    weapon = other.transform.GetChild(i).gameObject;
                }
            }

            Destroy(weapon.GetComponent<Shotgun>());
            Weapon newWeapon = weapon.AddComponent<Pistol>();
            weapon.GetComponent<Shoot>().updateWeapon(newWeapon);
            Destroy(gameObject);
        }
    }
}
