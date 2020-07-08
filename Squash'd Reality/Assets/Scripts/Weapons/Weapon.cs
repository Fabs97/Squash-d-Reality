using UnityEngine;


public class Weapon : MonoBehaviour {
    [Range(0, 50f)][SerializeField] protected float spread = 10f;
    [Range(0, 10f)][SerializeField] protected float fireRatioTime = 2f;
    [Range(0, 30f)][SerializeField] protected float bulletForce = 20f;
    [SerializeField] protected GameObject bulletPrefab;

    protected Transform _firePoint;

    public virtual void Start() {
        _firePoint = transform.GetChild(0).transform;
    }
    public virtual void shoot(){ }
}