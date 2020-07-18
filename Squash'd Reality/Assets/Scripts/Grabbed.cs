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
    
    private void Start()
    {
        grabPos = null;
    }

    private void Update()
    {
        
        if (grabPos != null)
        {
            gameObject.transform.parent = grabPos.transform;

        }
        else
        {
            gameObject.transform.parent = null;

        }
        if (isServer)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }

        if (!hasAuthority)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;   
        }
        
    }
}
