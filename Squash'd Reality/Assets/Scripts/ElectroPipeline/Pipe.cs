using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pipe : NetworkBehaviour
{ 
    RaycastHit ray1;
    RaycastHit ray2;
    int layerMask = 1 << 30;
    bool is1Hitting;
    bool is2Hitting;
    float maxDist = 0.15f;

    [SerializeField] private float snapValue = 1.0f;

    [SyncVar] public bool firstOrEnd;
    //[HideInInspector]
    [SyncVar(hook="_isConnectedChanged")] public bool isConnected;

    [SerializeField] private Material connectedMaterial;
    private Material unconnectedMaterial;
    private int holesOnMe = 0;
    private List<bool> holesAnswers;
    private bool considerHoleAnswers = true;
    private MeshRenderer meshRenderer;
    private ElectroPipeline pipelineChallengeScript;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        pipelineChallengeScript = Object.FindObjectOfType<ElectroPipeline>();
        unconnectedMaterial = meshRenderer.material;
        setPipeConnected(false);
        
        holesAnswers = new List<bool>();
        foreach(Transform child in transform){
            if(child.gameObject.tag == "Hole") holesOnMe ++;
        }

        if (isServer)
        {
            firstOrEnd = false;
        }
    }

    void Update()
    {
        /*if (!isConnected)
        {
            foreach (Transform child in transform) {
                if(child.gameObject.tag == "Hole") {
                    child.gameObject.GetComponent<Hole>().checkHoleConnection2();
                }
            }
        }*/
    }

    public void _isConnectedChanged(bool connected)
    {
        isConnected = connected;
       // Debug.LogError("isConnected: " + connected);
        this.meshRenderer.material = connected ? connectedMaterial : unconnectedMaterial;
    }

    public void setPipeConnected(bool connected){
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetPipeConnected(gameObject, connected);
        /*if(considerHoleAnswers){
            if(connected) {
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetPipeConnected(gameObject, connected);
                considerHoleAnswers = false;
            }
        } else {
            holesAnswers.Add(connected);
            if(holesAnswers.Count == holesOnMe){
                // all hits go to false
                considerHoleAnswers = true;
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetPipeConnected(gameObject, false);
                holesAnswers = new List<bool>();
            } 
        }*/
    }

    public void ensureConnection(){
        bool atLeastOneConnection = false;
        foreach(Transform child in transform){
            if(child.gameObject.tag == "Hole"){
                Hole hole = child.gameObject.GetComponent<Hole>();
                RaycastHit hit = hole.fireHoleRaycast();
                if(hit.collider != null){
                    atLeastOneConnection = true;
                }
            }
        }
        setPipeConnected(atLeastOneConnection);  
    }

    public void releasedPipe(){
        bool previousFirstOrEnd = firstOrEnd;
        float x = Mathf.Round(gameObject.transform.position.x / snapValue);
        float y = 0.55f;
        float z = Mathf.Round(gameObject.transform.position.z / snapValue);
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetTransformTo(gameObject, new Vector3(x,y,z));
        foreach (Transform child in transform) {
            if(child.gameObject.tag == "Hole") {
                child.gameObject.GetComponent<Hole>().checkHoleConnection();
            }
        }

        bool latestFirstOrEnd = firstOrEnd;
        if (previousFirstOrEnd && !latestFirstOrEnd)
        {
            GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
            foreach (GameObject pipe in pipes)
            {
                if (pipe.transform.name != "PipeLineStart" && pipe.transform.name != "PipeLineEnd")
                {
                    GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetPipeConnected(pipe.gameObject, false);
 
                }
           
            }
        }

        StartCoroutine(pipeReleasedCoroutine());

    }
    
    

    IEnumerator pipeReleasedCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipe in pipes)
        {
            if (pipe.transform.name != "PipeLineStart" && pipe.transform.name != "PipeLineEnd")
            {
                pipe.GetComponent<Pipe>().allPipeReleased();  
            }
           
        }
        yield return new WaitForSeconds(0.2f);
        foreach (GameObject pipe in pipes)
        {
            if (pipe.transform.name != "PipeLineStart" && pipe.transform.name != "PipeLineEnd")
            {
                pipe.GetComponent<Pipe>().allPipeReleased();  
            }
           
        }

    }

    public void allPipeReleased()
    {
        int holes = 0;
        foreach (Transform child in transform) {
            if(child.gameObject.tag == "Hole") {
                holes = holes + child.gameObject.GetComponent<Hole>().checkIntHoleConnection();
            }
        }

        Debug.LogError("PIPE: " + gameObject.transform.name);
        if (holes == 0)
        {
            Debug.LogError("HOLES: " + holes);
            setPipeConnected(false);
        }
        else
        {
            Debug.LogError("HOLES: " + holes);
            setPipeConnected(true);
        }
    }

    public void checkLine(){
        pipelineChallengeScript.checkLine();
    }

    public void checkNextStep(){
        foreach(Transform child in transform){
            if(child.gameObject.tag == "Hole") {
                RaycastHit nextHit = child.gameObject.GetComponent<Hole>().fireHoleRaycast();
                if(nextHit.collider != null){ // if != null, then it is a Raycast
                    GameObject nextGO = nextHit.collider.gameObject;
                    if(!pipelineChallengeScript.alreadyChecked(nextGO.transform.parent.gameObject)){
                        if(nextGO != null){
                            pipelineChallengeScript.addToFinalPath(gameObject);

                            if(nextGO.tag == "HoleEnd") {
                                pipelineChallengeScript.lightUpPath();
                            } else if(nextGO.tag != "HoleEnd") {
                                nextHit.collider.gameObject.GetComponentInParent<Pipe>().checkNextStep();
                            }
                        }
                    }
                } else {
                    // the raycast did not hit anything, it is failing. 
                    // TODO: handle the case where all raycasts fail (?)

                }
            }
        }
    }

    public void setFirstOrEnd(bool value)
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>()
            .CmdSetFirstOrEndElectroPipeline(gameObject,value);
    }
}
