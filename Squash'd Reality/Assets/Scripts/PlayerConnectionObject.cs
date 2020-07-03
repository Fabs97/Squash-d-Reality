using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour {

	void Start () {
        if(!isLocalPlayer) return;

        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");
        CmdSpawnMyUnit();
	}

    public GameObject PlayerUnitPrefab;
	
	void Update () {
		
        if(!isLocalPlayer) return;

        if( Input.GetKeyDown(KeyCode.S) ) {
            CmdSpawnMyUnit();
        }
	}


    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(PlayerUnitPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}
