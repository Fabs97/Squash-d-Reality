using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{ 
    
    public GameObject hole1;
    public GameObject hole2;

    RaycastHit ray1;
    RaycastHit ray2;
    int layerMask = 1 << 30;

    bool is1Hitting;
    bool is2Hitting;

    float maxDist = 0.15f;

    [HideInInspector]
    public bool isConnected = false;
    public Material mat;

    private void Start()
    {
        if (gameObject.tag == "Start") isConnected = true;
    }

    void Update()
    {
        
    }

    public void PipeCheck()
    {
        is1Hitting = Physics.Raycast(hole1.transform.position, -hole1.transform.right, out ray1, maxDist, layerMask);
        is2Hitting = Physics.Raycast(hole2.transform.position, -hole2.transform.right, out ray2, maxDist, layerMask);

        if (is1Hitting)
        {
            Debug.Log("Pipe::Update -- hole1 hit, I am" + gameObject.name);
            ConnectionCheck(ray1.collider);
        }

        if (is2Hitting)
        {
            Debug.Log("Pipe::Update -- hole2 hit, I am" + gameObject.name);
            ConnectionCheck(ray2.collider);
        }
    }

    void ConnectionCheck(Collider collider)
    {
        if (collider.GetComponent<Pipe>().isConnected)
        {
            isConnected = true;
            GetComponent<MeshRenderer>().material = mat;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(hole1.transform.position, -hole1.transform.right * maxDist);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(hole2.transform.position, -hole2.transform.right * maxDist);       
    }
}
