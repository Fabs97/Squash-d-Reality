using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    RaycastHit hit;
    bool hitDetect;
    int layerMask = 1 << 31;
    float maxDist = 0.5f;
   // [HideInInspector]
    public bool isPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hitDetect = Physics.Raycast(transform.position, transform.up, out hit, maxDist, layerMask);
        if (hitDetect)
        {
            isPressed = true;
        }
        else isPressed = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * maxDist);

    }
}
