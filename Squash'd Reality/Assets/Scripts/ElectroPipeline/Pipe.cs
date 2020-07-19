using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{ 
    RaycastHit ray1;
    RaycastHit ray2;
    int layerMask = 1 << 30;
    bool is1Hitting;
    bool is2Hitting;
    float maxDist = 0.15f;

    [SerializeField] private float snapValue = 1.0f;

    [HideInInspector]
    public bool isConnected = false;
    public Material mat;

    private void Start()
    {

    }

    void Update()
    {
        
    }

    public void releasedPipe(){
        Debug.LogError("RELEASED PIPE");
        float x = Mathf.Round(gameObject.transform.position.x / snapValue);
        float y = gameObject.transform.position.y;
        float z = Mathf.Round(gameObject.transform.position.z / snapValue);
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetTransformTo(gameObject, new Vector3(x,y,z), Quaternion.identity);
      /*  foreach (Transform child in transform) {
            if(child.gameObject.tag == "Hole") {
                child.gameObject.GetComponent<Hole>().checkHoleConnection();
            }
        }*/
    }
}
