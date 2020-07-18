using UnityEngine;
using UnityEngine.Networking;

public class Grabber : NetworkBehaviour
{
    RaycastHit hit;
    RaycastHit hit1;
    RaycastHit hit2;
    private LevelManager.LevelManager _levelManager;

    GameObject toGrab;
    private Light light;
    private float luminosity = 10;
    private bool isGrabbing = false;
    private bool needToToggleLight = false;

    bool hitDetect;
    bool hitDetect1;
    bool hitDetect2;
    [SerializeField] private float maxDist = 0.5f;
    [SerializeField] private GameObject grabbedPosObj;
    int layerMask = 1 << 31;

    // private float throwForce = 400f;
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

        needToToggleLight = _levelManager.getCurrentLevel().isDark;
        Debug.Log("Grabber::Start - needToToggleLight? " + needToToggleLight);
        if (needToToggleLight) {
            askToggleLight(true);
        }
        else {
            light.intensity = 0f;
        }
    }

    // Update is called once per frame
    void Update() {
        if (hasAuthority) {
            Grab();
        }
    }

    void Grab()
    {
        if (Input.GetButton("Interact"))
        {
            Debug.Log("Interact");
            hitDetect = Physics.Raycast(transform.position, transform.forward, out hit, maxDist, layerMask);
            hitDetect1 = Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit1, maxDist, layerMask);
            hitDetect2 = Physics.Raycast(transform.position + new Vector3(0, -0.5f, 0), transform.forward, out hit2, maxDist, layerMask);

            Debug.Log(""+hitDetect + hitDetect1 + hitDetect1);
            if (hitDetect)
                setToGrab(hit.collider.gameObject);
            else if (hitDetect1)
                setToGrab(hit1.collider.gameObject);
            else if (hitDetect2)
                setToGrab(hit2.collider.gameObject);
        }
        else {
            if (toGrab != null) {
                removeGrab();
            }
        }
    }

    public void removeGrab(){
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdRemoveAuthority(toGrab);
        // toGrab.GetComponent<BoxCollider>().enabled = true;
      //  handleGrabbedRb(toGrab, true);

        // grabbedRb.AddForce(transform.forward * throwForce);
        if(toGrab.tag == "Pipe"){
            // TODO: dynamic x and z values
            toGrab.GetComponent<Pipe>().releasedPipe();
        }

        toGrab.GetComponent<Grabbed>().grabPos = null;
        toGrab = null;
        isGrabbing = false;  
        if(needToToggleLight) askToggleLight(true);
    }

    public void toggleLight(bool val){
        if(_levelManager.getCurrentLevel().isDark){
            light.intensity = val ? luminosity : 0;
        }
    }

    private void setToGrab(GameObject go)
    {
        Debug.Log("SET to grab");
        toGrab = go;
        if (!isGrabbing)
        {
            Debug.Log("Grabber::setToGrab - assigning authority");
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdAssignAuthority(go);
        }
        // toGrab.GetComponent<BoxCollider>().enabled = false;
       // handleGrabbedRb(toGrab, false);


        //toGrab.transform.parent = transform;
        toGrab.GetComponent<Grabbed>().grabPos = grabbedPosObj;
        isGrabbing = true;
        if(needToToggleLight) askToggleLight(false);
    }

    private void handleGrabbedRb(GameObject go, bool release){
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetGrabbedRigidBody(go, release);
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
        GameObject localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
        if (localPlayer != null) {

            string playerName = GetComponentInParent<DummyMoveset>().playerName;
            if ( playerName == "Markus Nobel") localPlayer.GetComponent<PlayerController>().CmdsetLight1(value);
            else if (playerName == "Ken Nolo") localPlayer.GetComponent<PlayerController>().CmdsetLight2(value);
            else if (playerName == "Kam Brylla") localPlayer.GetComponent<PlayerController>().CmdsetLight3(value);
            else if (playerName == "Raphael Nosun") localPlayer.GetComponent<PlayerController>().CmdsetLight4(value);
        }
    }
}
