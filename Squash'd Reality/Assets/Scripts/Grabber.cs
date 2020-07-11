using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Grabber : NetworkBehaviour
{
    RaycastHit hit;
    RaycastHit hit1;
    RaycastHit hit2;
    LevelManager.LevelManager _levelManager;

    GameObject toGrab;
    private Light light;
    private float luminosity = 10;
    private bool isGrabbing = false;

    bool hitDetect;
    bool hitDetect1;
    bool hitDetect2;
    public float maxDist = 2f;
    int layerMask = 1 << 31;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = Object.FindObjectOfType<LevelManager.LevelManager>();
        light = GetComponent<Light>();
        if(light != null) light.intensity = _levelManager.getCurrentLevel().isDark ? luminosity : 0;
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
                removeGrab();
            }
        }
    }

    public void removeGrab(){
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdRemoveAuthority(toGrab);
        toGrab.GetComponent<Rigidbody>().isKinematic = true;
        toGrab.transform.parent = null;
        toGrab = null;
        isGrabbing = false;  
        toggleLight(true);
    }

    void toggleLight(bool val){
        if(_levelManager.getCurrentLevel().isDark){
            light.intensity = val ? luminosity : 0;
        }
    }

    private void setToGrab(GameObject go)
    {
        toGrab = go;
        if (!isGrabbing)
        {
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdAssignAuthority(go);
        }
        toGrab.GetComponent<Rigidbody>().isKinematic = false;
        toGrab.transform.parent = transform;
        isGrabbing = true;
        toggleLight(false);
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
