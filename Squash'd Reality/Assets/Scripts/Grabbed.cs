using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;

public class Grabbed : NetworkBehaviour
{
    public GameObject grabPos;
    [SyncVar] public Vector3 position;
    [SyncVar] public Quaternion rotation;
    public int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    private void Start()
    {
        grabPos = null;
        if (isServer)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }
    }

    private void Update()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        
        if (grabPos != null)
        {
            gameObject.transform.parent = grabPos.transform;

        }
        else
        {
            gameObject.transform.parent = null;

        }
        if (isServer && hasAuthority)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }else if (isClient && hasAuthority && grabPos!=null)
        {
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetPos(this.gameObject, grabPos.transform.position, Quaternion.identity);
            
        }

        if (!hasAuthority)
        {
           gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,position, 0.1f);
           gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, 0.1f);   
        }
        
        
    }
}
