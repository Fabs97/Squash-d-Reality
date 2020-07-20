using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    int layerMask = 1 << 30;
    private const float raycastDistance = 0.4f;

    private Dictionary<GameObject, Vector3> connectedTo;
    private Pipe parent;
    private void Start() {
        parent = GetComponentInParent<Pipe>();
    }

    public void checkHoleConnection(){
        RaycastHit raycastHit;
        Debug.Log("cast ray from: " + gameObject.name);
        if(Physics.Raycast(transform.position, transform.right, out raycastHit, raycastDistance, layerMask)){
            Debug.Log("Hole::checkHoleConnection - hit gameObject with name: " + raycastHit.collider.gameObject.name);
            GameObject otherHole = raycastHit.collider.gameObject;
            Hole otherHoleScript = otherHole.GetComponent<Hole>();
            // TODO: can start and end be the same? if so, put it in OR
            if(otherHole.tag == "HoleStart"){
                // Connected to the starting hole, defaults to true
                parent.setPipeConnected(true);
            }
            else if(otherHole.tag == "HoleEnd"){
                // Connected to the ending hole, defaults to true
                parent.setPipeConnected(true);

            }
            else{
                // Connected to an intermediate hole
                parent.ensureConnection();
                otherHoleScript.isPipeConnected();
            }
        } 
        else{
            // released and did not hit anything, so this dies.
            parent.setPipeConnected(false);
        }
    }

    public bool isPipeConnected(){
        return this.parent.isConnected;
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