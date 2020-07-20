using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    int layerMask = 1 << 30;
    private const float raycastDistance = 0.2f;

    private Dictionary<GameObject, Vector3> connectedTo;
    private Pipe parent;
    private void Start() {
        parent = GetComponentInParent<Pipe>();
    }

    public void checkHoleConnection(){
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.right, out raycastHit, raycastDistance, layerMask)){
            Debug.Log("Hole::checkHoleConnection - hit gameObject with name: " + raycastHit.collider.gameObject.name);
            
            // TODO: can start and end be the same? if so, put it in OR
            if(raycastHit.collider.gameObject.tag == "HoleStart"){
                // Connected to the starting hole, defaults to true
                parent.setPipeConnected(true);
            }
            else if(raycastHit.collider.gameObject.tag == "HoleEnd"){
                // Connected to the ending hole, defaults to true
                parent.setPipeConnected(true);

            }
            else{
                // Connected to an intermediate hole
                parent.ensureConnection();
            }

        }
    }

    public RaycastHit fireHoleRaycast(){
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.right, out hit, raycastDistance, layerMask);
        return hit;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.right * raycastDistance);
    }

    private void recheckConnections(){
        foreach (var connection in connectedTo) {
            RaycastHit hit;
            if(!Physics.Raycast(transform.position, connection.Value, out hit, raycastDistance, layerMask)){
                connectedTo.Remove(connection.Key);
            }
        }
    }
}