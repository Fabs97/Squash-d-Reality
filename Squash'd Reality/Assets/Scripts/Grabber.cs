using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Grabber : NetworkBehaviour
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
        if (hasAuthority)
        {
            Grab();
        }
    }

    void Grab()
    {
        if (Input.GetButton("Interact"))
        {

            hitDetect = Physics.Raycast(transform.position, transform.forward, out hit, maxDist, layerMask);
            hitDetect1 = Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit1, maxDist, layerMask);
            hitDetect2 = Physics.Raycast(transform.position + new Vector3(0, -0.5f, 0), transform.forward, out hit2, maxDist, layerMask);

            if (hitDetect)
                setToGrab(hit.collider.gameObject);
            else if (hitDetect1)
                setToGrab(hit1.collider.gameObject);
            else if (hitDetect2)
                setToGrab(hit2.collider.gameObject);
        }
        else
        {
            if (toGrab != null)
            {
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdRemoveAuthority(toGrab);
                toGrab.transform.parent = null;
                toGrab = null;
                isGrabbing = false;  
            }
        }
    }

    private void setToGrab(GameObject go)
    {
        toGrab = go;
        if (!isGrabbing)
        {
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdAssignAuthority(go);
        }
        toGrab.transform.parent = transform;
        isGrabbing = true;
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
