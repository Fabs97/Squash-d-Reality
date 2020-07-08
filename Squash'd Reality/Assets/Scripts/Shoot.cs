using UnityEngine;

public class Shoot : MonoBehaviour
{
    //-------------------SHOOTING SETTINGS-------------------
    private bool canShoot = true; //TODO: true only for debugging, must be false

    private Weapon weapon;

    void Start()
    {
        weapon = gameObject.GetComponent<Weapon>();
    }

    private void Update()
    {
        if (Input.GetAxis("Fire")!=0 && canShoot)
        {
            // StartBulletEmission();
            weapon.shoot();
        }
    }
    
    public void CanPlayerShoot(bool value)
    {
        canShoot = value;
    }
}
