using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class Weapon : MonoBehaviour {
    [Range(0, 50f)][SerializeField] protected float spread = 10f;
    [Range(0, 10f)][SerializeField] protected float fireRatioTime = 1f;
    [Range(0, 30f)][SerializeField] protected float bulletForce = 20f;
    [Range(0, 10)][SerializeField] protected int numberOfBullets = 1;
    public bool canShoot; 
    protected string bulletName;

    protected Transform _firePoint;

    public virtual void Start() {
        _firePoint = transform.GetChild(0).transform;
    }
    
    public virtual void shoot()
    {
        if (GetComponentInParent<DummyMoveset>().hasAuthority && canShoot)
        {
            canShoot = false;
            BulletInstantiation(GetComponentInParent<DummyMoveset>().playerName);
            StartCoroutine(fireRatio());   
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