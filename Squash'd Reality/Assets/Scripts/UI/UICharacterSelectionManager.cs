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
    [SerializeField] private GameObject Character1Prefab;
    [SerializeField] private GameObject Character2Prefab;
    [SerializeField] private GameObject Character3Prefab;
    [SerializeField] private GameObject Character4Prefab;
    [SerializeField] private Transform Character1Transform;
    [SerializeField] private Transform Character2Transform;
    [SerializeField] private Transform Character3Transform;
    [SerializeField] private Transform Character4Transform;

    //Networking variables
    [SyncVar] private bool Character1Taken = false;
    [SyncVar] private bool Character2Taken = false;
    [SyncVar] private bool Character3Taken = false;
    [SyncVar] private bool Character4Taken = false;
    
    //local network variables
    private bool Character1TakenLocal = false;
    private bool Character2TakenLocal = false;
    private bool Character3TakenLocal = false;
    private bool Character4TakenLocal = false;

    private SceneLoader.SceneLoader _sceneLoader;
  
    private void Start() {
        _sceneLoader = FindObjectOfType<SceneLoader.SceneLoader>();
       
    }
    private void Update()
    {
        UpdateNetworkVariables();

    }

    public void UpdateNetworkVariables()
    {
        if (Character1TakenLocal != Character1Taken)
        {
            Character1TakenLocal = Character1Taken;
            setCharacter1Active(Character1TakenLocal);
        }
        if (Character2TakenLocal != Character2Taken)
        {
            Character2TakenLocal = Character2Taken;
            setCharacter2Active(Character2TakenLocal);
        }
        if (Character3TakenLocal != Character3Taken)
        {
            Character3TakenLocal = Character3Taken;
            setCharacter3Active(Character3TakenLocal);
        }
        if (Character4TakenLocal != Character4Taken)
        {
            Character4TakenLocal = Character4Taken;
            setCharacter4Active(Character4TakenLocal);
        }
        
    }
    public void setCharacter1Active(bool value)
    {
        if (value)
        {
            Character1.GetComponent<Image>().color = Color.red;
            Character1.interactable = !value;
        }
        else
        {
            Character1.GetComponent<Image>().color = Color.clear;
            Character1.interactable = value;
        }
    }
    
    void setCharacter2Active(bool value)
    {
        if (value)
        {
            Character2.GetComponent<Image>().color = Color.red;
            Character2.interactable = !value;
        }
        else
        {
            Character2.GetComponent<Image>().color = Color.clear;
            Character2.interactable = value;
        }
    }

    void setCharacter3Active(bool value)
    {
        if (value)
        {
            Character3.GetComponent<Image>().color = Color.red;
            Character3.interactable = !value;
        }
        else
        {
            Character3.GetComponent<Image>().color = Color.clear;
            Character3.interactable = value;
        }
    }

    void setCharacter4Active(bool value)
    {
        if (value)
        {
            Character4.GetComponent<Image>().color = Color.red;
            Character4.interactable = !value;
        }
        else
        {
            Character4.GetComponent<Image>().color = Color.clear;
            Character4.interactable = value;
        }
    }

    void showCharacterAlreadyChoosen()
    {
        CharacterAlreadyChoosen.SetActive(true);
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
        
        
        if (characterName == "Character1") { 
            if(Character1Taken)
            {
                showCharacterAlreadyChoosen();
            }
            else
            {
                Character1Taken = true;
                //loadLobby(Character1Prefab, Character1Transform);    
            }
            
        } else if (characterName == "Character2") {
            if (Character2Taken)
            {
                showCharacterAlreadyChoosen();
            }
            else
            {
                Character2Taken = true;
                // loadLobby(Character2Prefab, Character2Transform); 
            }
            
        } else if (characterName == "Character3") {
            if(Character3Taken)
            {
                showCharacterAlreadyChoosen();
            }
            else
            {
                Character3Taken = true;
                // loadLobby(Character3Prefab, Character3Transform);  
            }
        } else if (characterName == "Character4") {
            if (Character4Taken)
            {
                showCharacterAlreadyChoosen();
            }
            else
            {
                Character4Taken = true;
                //loadLobby(Character4Prefab, Character4Transform);   
            }
           
        }

       

    }
    
    private void loadLobby(GameObject c, Transform t){
        _sceneLoader.loadNextScene("Lobby");
        CmdSpawnPlayer(c);
    }
    
    
    [Command]
    public void CmdSpawnPlayer(GameObject playerPrefab) {
        GameObject go = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        NetworkServer.Spawn(go);
        Debug.Log("SpawnManager::CmdSpawnPlayer - Spawned my player!");
    }
    
    
}
