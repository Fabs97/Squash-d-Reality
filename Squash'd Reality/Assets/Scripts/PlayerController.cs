using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //LOCAL PLAYER OBJECT DEFINITION --> AUTHORITATIVE ON SERVER
        if (isClient && isLocalPlayer)
        {
            gameObject.tag = "LocalPlayer";
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
    }
    
    [Command]
    public void CmdSpawnPlayer(GameObject playerPrefab) {
        GameObject go = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        NetworkServer.Spawn(go);
        Debug.Log("SpawnManager::CmdSpawnPlayer - Spawned my player!");
    }   
    
}
