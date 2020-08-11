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

    [SyncVar] public bool isFirst;

    [SyncVar] public bool isEnd;
    
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
        pipelineChallengeScript = FindObjectOfType<ElectroPipeline>();
        unconnectedMaterial = meshRenderer.material;
        setPipeConnected(false);
        
        holesAnswers = new List<bool>();
        foreach(Transform child in transform){
            if(child.gameObject.tag == "Hole") holesOnMe ++;
        }

        if (isServer)
        {
            isFirst = false;
        }
    }
    
    public void _isConnectedChanged(bool connected)
    {
        isConnected = connected;
        this.meshRenderer.material = connected ? connectedMaterial : unconnectedMaterial;
    }

    public void setPipeConnected(bool connected)
    {
        GameObject localPlayer;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
        if (localPlayer != null)
        {
            localPlayer.GetComponent<PlayerController>().CmdSetPipeConnected(gameObject, connected);

        }
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
        float x = Mathf.Round(gameObject.transform.position.x / snapValue);
        float y = 0.55f;
        float z = Mathf.Round(gameObject.transform.position.z / snapValue);
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetTransformTo(gameObject, new Vector3(x,y,z));

        if (isServer)
        {
            int start = 0;
            int end = 0;
            foreach (Transform child in transform) {
                if(child.gameObject.tag == "Hole") {
                    child.gameObject.GetComponent<Hole>().checkHoleConnection();
                    start = start + child.gameObject.GetComponent<Hole>().checkIntStart();
                    end = end + child.gameObject.GetComponent<Hole>().checkIntEnd();
                }
            }

            if (start == 0)
            {
                setFirst(false);
            }
            if (start == 1)
            {
                setFirst(true);
            }

            if (end == 0)
            {
                setEnd(false);
            }
            if (end == 1)
            {
                setEnd(true);
            }
        }
        else
        {
            StartCoroutine(pipeStartEnd());

        }
        StartCoroutine(pipeReleasedCoroutine());


    }



    IEnumerator pipeStartEnd()
    {
        yield return new WaitForSeconds(0.2f);
        int start = 0;
        int end = 0;
        foreach (Transform child in transform) {
            if(child.gameObject.tag == "Hole") {
                child.gameObject.GetComponent<Hole>().checkHoleConnection();
                start = start + child.gameObject.GetComponent<Hole>().checkIntStart();
                end = end + child.gameObject.GetComponent<Hole>().checkIntEnd();
            }
        }

        if (start == 0)
        {
            setFirst(false);
        }
        if (start == 1)
        {
            setFirst(true);
        }

        if (end == 0)
        {
            setEnd(false);
        }
        if (end == 1)
        {
            setEnd(true);
        }
        
        //-------------
        yield return new WaitForSeconds(0.2f);
         start = 0;
         end = 0;
        foreach (Transform child in transform) {
            if(child.gameObject.tag == "Hole") {
                child.gameObject.GetComponent<Hole>().checkHoleConnection();
                start = start + child.gameObject.GetComponent<Hole>().checkIntStart();
                end = end + child.gameObject.GetComponent<Hole>().checkIntEnd();
            }
        }

        if (start == 0)
        {
            setFirst(false);
        }
        if (start == 1)
        {
            setFirst(true);
        }

        if (end == 0)
        {
            setEnd(false);
        }
        if (end == 1)
        {
            setEnd(true);
        }
    }
    IEnumerator pipeReleasedCoroutine()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        for (int i = 0; i < pipes.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject pipe in pipes)
            {
                if (pipe.transform.name != "PipeLineStart" && pipe.transform.name != "PipeLineEnd")
                {
                    pipe.GetComponent<Pipe>().allPipeReleased();  
                }
           
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

        if (holes == 0)
        {
            setPipeConnected(false);
        }
        else
        {
            setPipeConnected(true);
        }
    }
    
    public void setFirst(bool value)
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>()
            .CmdSetFirstElectroPipeline(gameObject,value);
    }

    public void setEnd(bool value)
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>()
            .CmdSetEndPipeline(gameObject,value);
    }
}
