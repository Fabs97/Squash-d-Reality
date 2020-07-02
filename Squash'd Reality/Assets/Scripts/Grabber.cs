using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    RaycastHit hit;
    RaycastHit hit1;
    RaycastHit hit2;

    GameObject toGrab;

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
                if (hit.collider.gameObject.tag == "Grabbable")
                {
                    toGrab = hit.collider.gameObject;
                    toGrab.transform.parent = transform;
                    toGrab.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

            else if (hitDetect1)
            {
                if (hit1.collider.gameObject.tag == "Grabbable")
                {
                    toGrab = hit1.collider.gameObject;
                    toGrab.transform.parent = transform;
                    toGrab.GetComponent<Rigidbody>().isKinematic = true;

                }
            }

            else if (hitDetect2)
            {
                if (hit2.collider.gameObject.tag == "Grabbable")
                {
                    toGrab = hit2.collider.gameObject;
                    toGrab.transform.parent = transform;
                    toGrab.GetComponent<Rigidbody>().isKinematic = true;

                }
            }
        }

        else
        {
            toGrab.GetComponent<Rigidbody>().isKinematic = false;
            toGrab.transform.parent = null;
            toGrab = null;
        }
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
