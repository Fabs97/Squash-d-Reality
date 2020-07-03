using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UICharacterSelectionManager : NetworkBehaviour
{
    //UI elements
    [SerializeField] private  Button Character1;
    [SerializeField] private Button Character2;
    [SerializeField] private Button Character3;
    [SerializeField] private Button Character4;
    [SerializeField] private GameObject CharacterAlreadyChoosen;


    //Networking variables
    [SyncVar] private bool Character1Taken = false;
    [SyncVar] private bool Character2Taken = false;
    [SyncVar] private bool Character3Taken = false;
    [SyncVar] private bool Character4Taken = false;
    

    private void Update()
    {
        if (Character1Taken)
        {
            Debug.LogError("CAMBIATE");
        }
    }

    public void setCharacter1Active(bool value)
    {
    
        Character1.gameObject.SetActive(value);
    }
    
    void setCharacter2Active(bool value)
    {
        Character2.gameObject.SetActive(value); 
    }

    void setCharacter3Active(bool value)
    {
        Character3.gameObject.SetActive(value);
    }

    void setCharacter4Active(bool value)
    {
        Character4.gameObject.SetActive(value);
    }

    void showCharacterAlreadyChoosen()
    {
        StartCoroutine(countdownDisappereance());
    }

    IEnumerator countdownDisappereance()
    {
        yield return new WaitForSeconds(2f);
        CharacterAlreadyChoosen.SetActive(false);
    }
    //SELECT character is ok
    [Command] //--> the command is sent from client to server
    void CmdSelectCharacter(string characterName)
    {
        if (characterName == "Character1")
        { 
            Character1Taken = true;
        }else if (characterName == "Character2")
        {
            Character2Taken = true;
        }else if (characterName == "Character3")
        {
            Character3Taken = true;
        }else if (characterName == "Character4")
        {
            Character4Taken = true;
        }
    }
    
}
