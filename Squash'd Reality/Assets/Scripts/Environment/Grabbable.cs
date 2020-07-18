using UnityEngine;
using UnityEngine.Networking;

public class Grabbable : NetworkBehaviour {
    [SyncVar] public bool detectCollisions = true;
    [SyncVar] public bool useGravity = true;

    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        _rb.detectCollisions = detectCollisions;
        _rb.useGravity = useGravity;
    }
    
    public void setRigidBody(){
        Debug.Log("Diocane vediamo se vengo chiamato ovunque.");
        _rb.detectCollisions = detectCollisions;
        _rb.useGravity = useGravity;
    }
}