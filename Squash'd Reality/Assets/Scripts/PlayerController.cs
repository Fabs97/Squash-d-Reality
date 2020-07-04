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
        if (isClient && !isServer && isLocalPlayer)
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
        if (characterName == "Character1")
        {
            GameObject.Find("UICharacterSelectionManager").GetComponent<UICharacterSelectionManager>().Character1Taken =
                true;
        }
    }
    
    
    
}
