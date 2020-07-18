using UnityEngine;
using UnityEngine.Networking;

public class Grabbable : NetworkBehaviour {
    [SyncVar] public bool detectCollisions;
    [SyncVar] public bool useGravity;

    private Rigidbody _rb;

    private void Start() {
        if (isServer)
        {
            detectCollisions = true;
            useGravity = true;
        }
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        _rb.detectCollisions = detectCollisions;
        _rb.useGravity = useGravity;
    }
    
}