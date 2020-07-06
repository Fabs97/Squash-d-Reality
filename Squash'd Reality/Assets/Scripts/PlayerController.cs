using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    private GameObject playerPrefab;
    private NetworkingManager.NetworkingManager _networkingManager;
    void Start()
    {
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        //LOCAL PLAYER OBJECT DEFINITION --> AUTHORITATIVE ON SERVER
        if (isClient && isLocalPlayer)
        {
            gameObject.tag = "LocalPlayer";
            gameObject.transform.SetParent(GameObject.Find("__app").transform); 
        }

        if (isServer)
        {
            playerPrefab = _networkingManager.prefabList()[0];
        }


    }

    // Update is called once per frame
    void Update()
    {
      
    }


    [Command]
    public void CmdSelectedCharacter(string characterName)
    {
        gameObject.name = characterName;

        UICharacterSelectionManager uICharacterSelectionManager = GameObject.Find("UICharacterSelectionManager").GetComponent<UICharacterSelectionManager>();
        if (characterName == "Character1") uICharacterSelectionManager.Character1Taken = true;
        else if (characterName == "Character2") uICharacterSelectionManager.Character2Taken = true;
        else if (characterName == "Character3") uICharacterSelectionManager.Character3Taken = true;
        else if (characterName == "Character4") uICharacterSelectionManager.Character4Taken = true;
        uICharacterSelectionManager.numCharactersChoosen++;
    }
    
    [Command]
    public void CmdSpawnPlayer() {
        GameObject go = Instantiate(playerPrefab, playerPrefab.transform);
        NetworkServer.Spawn(go);
        Debug.Log("SpawnManager::CmdSpawnPlayer - Spawned my player!");
    }   
    
}
