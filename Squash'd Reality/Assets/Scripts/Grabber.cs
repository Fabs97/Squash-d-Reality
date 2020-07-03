using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    RaycastHit hit;
    RaycastHit hit1;
    RaycastHit hit2;

    GameObject toGrab;
    private bool isGrabbing = false;

    bool hitDetect;
    bool hitDetect1;
    bool hitDetect2;
    public float maxDist = 2f;
    int layerMask = 1 << 31;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact"))
        {

            hitDetect = Physics.Raycast(transform.position, transform.forward, out hit, maxDist, layerMask);
            hitDetect1 = Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit1, maxDist, layerMask);
            hitDetect2 = Physics.Raycast(transform.position + new Vector3(0, -0.5f, 0), transform.forward, out hit2, maxDist, layerMask);

            if (hitDetect)
            {                
                toGrab = hit.collider.gameObject;
                toGrab.transform.parent = transform;
                toGrab.GetComponent<Rigidbody>().isKinematic = true;       
                isGrabbing = true;
            }

            else if (hitDetect1)
            {
                toGrab = hit1.collider.gameObject;
                toGrab.transform.parent = transform;
                toGrab.GetComponent<Rigidbody>().isKinematic = true;
                isGrabbing = true;
            }

            else if (hitDetect2)
            {                
                toGrab = hit2.collider.gameObject;
                toGrab.transform.parent = transform;
                toGrab.GetComponent<Rigidbody>().isKinematic = true;
                isGrabbing = true;
            }
        }

        else
        {
            toGrab.GetComponent<Rigidbody>().isKinematic = false;
            toGrab.transform.parent = null;
            toGrab = null;
            isGrabbing = false;
        }
    }

    public bool GetIsGrabbing()
    {
        return isGrabbing;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (hitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);            
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * maxDist);
            Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward * maxDist);
            Gizmos.DrawRay(transform.position + new Vector3(0, -0.5f, 0), transform.forward * maxDist);
        }
    }
}
