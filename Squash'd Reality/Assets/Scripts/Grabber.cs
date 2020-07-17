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
    [SerializeField] private float maxDist = 0.5f;
    int layerMask = 1 << 31;

    private float throwForce = 400f;
    // Start is called before the first frame update
    void Start()
    {
        _levelManager = Object.FindObjectOfType<LevelManager.LevelManager>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "PlayerLight")
            {
                light = transform.GetChild(i).gameObject.GetComponent<Light>();
            }
        }

        if (_levelManager.getCurrentLevel().isDark)
        {
            askToggleLight(true);
        }
        else
        {
            light.intensity = 0f;
        }
            
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
        Rigidbody grabbedRb = toGrab.GetComponent<Rigidbody>();
        grabbedRb.useGravity = true;
        grabbedRb.isKinematic = false;
        grabbedRb.AddForce(transform.forward * throwForce);
        if(toGrab.tag == "Pipe"){
            toGrab.GetComponent<Pipe>().PipeCheck();
        }
        
        toGrab.transform.parent = null;
        toGrab = null;
        isGrabbing = false;  
        askToggleLight(true);
    }

    public void toggleLight(bool val){
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
        toGrab.GetComponent<Rigidbody>().useGravity = false;
        toGrab.transform.parent = transform;
        isGrabbing = true;
        askToggleLight(false);
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

    public void askToggleLight(bool value)
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") != null)
        {
            string playerName = GetComponentInParent<DummyMoveset>().playerName;
            if ( playerName == "Markus Nobel")
            {
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdsetLight1(value);
            }
            else if (playerName == "Ken Nolo")
            {
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdsetLight2(value);
            }
            else if (playerName == "Kam Brylla")
            {
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdsetLight3(value);
            }
            else if (playerName == "Raphael Nosun")
            {
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdsetLight4(value);

            }
        }
        
    }
}
