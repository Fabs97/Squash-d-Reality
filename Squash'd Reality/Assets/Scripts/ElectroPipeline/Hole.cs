using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    int layerMask = 1 << 30;
    private const float raycastDistance = 0.3f;

    private Dictionary<GameObject, Vector3> connectedTo;
    private Pipe parent;
    private void Start() {
        parent = GetComponentInParent<Pipe>();
    }

    public int checkIntStart()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.right, out raycastHit, raycastDistance, layerMask)){
            GameObject otherHole = raycastHit.collider.gameObject;
            Hole otherHoleScript = otherHole.GetComponent<Hole>();
            if (otherHole.tag == "HoleStart")
            {
                return 1;

            }
            else
            {
                return 0;
            }
        } 
        else{
            
            return 0;
        }
    }
    public int checkIntEnd()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.right, out raycastHit, raycastDistance, layerMask)){
            GameObject otherHole = raycastHit.collider.gameObject;
            Hole otherHoleScript = otherHole.GetComponent<Hole>();
            if (otherHole.tag == "HoleEnd")
            {
                return 1;

            }
            else
            {
                return 0;
            }
        } 
        else{
            
            return 0;
        }
    }
    public int checkIntHoleConnection(){
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.right, out raycastHit, raycastDistance, layerMask)){
            GameObject otherHole = raycastHit.collider.gameObject;
            Hole otherHoleScript = otherHole.GetComponent<Hole>();
            if(otherHole.tag == "HoleStart" || otherHole.tag =="HoleEnd"){
                if (otherHole.tag == "HoleStart")
                {
                    return 1;

                }
                else
                {
                    return 0;
                }
                
            }
            else{
                if(otherHoleScript.isPipeConnected())
                {
                    return 1;
                } 
            }
        } 
        else{
            
            return 0;
        }

        return 0;
    }
    public void checkHoleConnection(){
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.right, out raycastHit, raycastDistance, layerMask)){
            GameObject otherHole = raycastHit.collider.gameObject;
            Hole otherHoleScript = otherHole.GetComponent<Hole>();
            // TODO: can start and end be the same? if so, put it in OR
            if(otherHole.tag == "HoleStart" || otherHole.tag =="HoleEnd"){
                // Connected to the starting hole, defaults to true
                // Connected to the ending hole, defaults to true

                if (otherHole.tag == "HoleStart")
                {
                    parent.setPipeConnected(true);
                    parent.setFirstOrEnd(true);
                    parent.setEnd(false);

                }else if (otherHole.tag == "HoleEnd")
                {
                    parent.setEnd(true);
                    parent.setFirstOrEnd(false);
                }
            }
            // else if(otherHole.tag == "HoleEnd"){
            //     parent.setPipeConnected(true);
            // }
            else{
                // Connected to an intermediate hole
                //parent.ensureConnection();
               // Debug.LogError("Hole::checkHoleConnection -- otherHoleScript.isPipeConnected? " + otherHoleScript.isPipeConnected());
                if(otherHoleScript.isPipeConnected()){
                    parent.setPipeConnected(true);
                    
                } else {
                    // TODO: something to do???
                  
                }
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
    
    //* Returns the raycast or null 
    public RaycastHit fireHoleRaycast(){
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.right, out hit, raycastDistance, layerMask);
        return hit;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.right * raycastDistance);
    }

    // private void recheckConnections(){
    //     foreach (var connection in connectedTo) {
    //         RaycastHit hit;
    //         if(!Physics.Raycast(transform.position, connection.Value, out hit, raycastDistance, layerMask)){
    //             connectedTo.Remove(connection.Key);
    //         }
    //     }
    // }
}