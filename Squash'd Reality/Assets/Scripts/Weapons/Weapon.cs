using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class Weapon : MonoBehaviour {
    [Range(0, 50f)][SerializeField] protected float spread = 10f;
    [Range(0, 10f)][SerializeField] protected float fireRatioTime = 1f;
    [Range(0, 30f)][SerializeField] protected float bulletForce = 20f;
    [Range(0, 10)][SerializeField] protected int numberOfBullets = 1;
    [Range(0, Mathf.Infinity)] [SerializeField] protected int magazine = 30;
    public bool canShoot; 
    protected string bulletName;

    protected Transform _firePoint;

    public virtual void Start() {
        _firePoint = transform.GetChild(0).transform;
    }
    
    public virtual void shoot()
    {
        PlayerMoveset parentMoveset = GetComponentInParent<PlayerMoveset>();
        if (parentMoveset.hasAuthority && canShoot)
        {
            canShoot = false;
            BulletInstantiation(parentMoveset.playerName);
            magazine--;
            StartCoroutine(fireRatio());   
        }
        if(magazine == 0)
        {
            //instantiate pistol
            UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
            GameObject oldWeapon = this.gameObject;
            Destroy(oldWeapon.GetComponent<Weapon>());
            Weapon newWeapon = gameObject.AddComponent<Pistol>();
            transform.GetComponent<Shoot>().updateWeapon(newWeapon);
            //update UI
            if (transform.parent.gameObject.GetComponent<PlayerMoveset>().hasAuthority)
            {
                uiManager.setWeaponImage("Pistol");
                uiManager.setWeaponActive(true);
            }


        }


    }
    
    private IEnumerator fireRatio()
    {
        yield return new WaitForSeconds(fireRatioTime);
        this.canShoot = true;
    }

   
    private void BulletInstantiation(string shooterName){
        for (int i = 0; i < numberOfBullets; i++)
        {
          
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSpawnBullets(_firePoint.position, _firePoint.rotation, spread, bulletForce, bulletName, shooterName);
            
        }
    }

    
}