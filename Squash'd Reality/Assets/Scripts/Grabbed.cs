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
        if (isServer)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }
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
        if (isServer && hasAuthority)
        {
            position = gameObject.transform.position;
            rotation = gameObject.transform.rotation;
        }else if (isClient && hasAuthority && grabPos!=null)
        {
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetPos(this.gameObject, grabPos.transform.position, Quaternion.identity);
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;   
        }

        if (!hasAuthority)
        {
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;   
        }
        
    }
}
