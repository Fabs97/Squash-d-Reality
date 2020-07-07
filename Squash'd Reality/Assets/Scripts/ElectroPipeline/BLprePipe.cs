using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BLprePipe : MonoBehaviour
{

    RaycastHit bottom;
    RaycastHit left;

    GameObject bottomNeighbour;
    GameObject leftNeighbour;

    bool hasBottomHit;
    bool hasLeftHit;

    float maxDist = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null)
        {
            hasBottomHit = Physics.Raycast(transform.position + new Vector3(0, 0, -0.5f) , -transform.forward, out bottom, maxDist);
            hasLeftHit = Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), -transform.right, out left, maxDist);
            
            if (hasBottomHit)
            {
                Debug.Log("BLprePipe::Update -- bottom hit");
            }
            if (hasLeftHit)
            {
                if(left.collider.tag == "Hole")
                {
                    Debug.Log("BLprePipe::Update -- left hit");
                }
               
            }
        }
    }
}
