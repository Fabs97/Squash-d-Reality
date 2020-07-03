using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    //SELECT character is ok
    [Command] //--> the command is sent from client to server
    void CmdSelectCharacter(string characterName)
    {
        if (characterName == "Character1")
        { 
            UICharacterSelectionManager.setCharacter1Active(true);
        }
        if (characterName == "Character2")
        {
            
        }
        if (characterName == "Character3")
        {
           
        }
        if (characterName == "Character4")
        {
            
        }
    }
}
