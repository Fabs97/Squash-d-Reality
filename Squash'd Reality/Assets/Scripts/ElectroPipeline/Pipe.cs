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

    [HideInInspector]
    [SyncVar(hook="_isConnectedChanged")] public bool isConnected;

    [SerializeField] private Material connectedMaterial;
    private Material unconnectedMaterial;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        unconnectedMaterial = meshRenderer.material;
        setPipeConnected(false);
    }

    void Update()
    {
        
    }

    public void _isConnectedChanged(bool connected){
        this.meshRenderer.material = connected ? connectedMaterial : unconnectedMaterial;
    }

    public void setPipeConnected(bool value){
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetPipeConnected(gameObject, value);
    }

    public void releasedPipe(){
        float x = Mathf.Round(gameObject.transform.position.x / snapValue);
        float y = gameObject.transform.position.y;
        float z = Mathf.Round(gameObject.transform.position.z / snapValue);
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetTransformTo(gameObject, new Vector3(x,y,z));
        foreach (Transform child in transform) {
            if(child.gameObject.tag == "Hole") {
                child.gameObject.GetComponent<Hole>().checkHoleConnection();
            }
        }
    }
}
