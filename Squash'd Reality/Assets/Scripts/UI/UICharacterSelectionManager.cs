using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UICharacterSelectionManager : NetworkBehaviour
{
    //PLAYER prefab
    [SerializeField] private GameObject playerObject;
    
    //UI elements
    [SerializeField] private  Button Character1;
    [SerializeField] private Button Character2;
    [SerializeField] private Button Character3;
    [SerializeField] private Button Character4;
    [SerializeField] private GameObject MessageBox;

    //Networking variables
    [SyncVar] public bool Character1Taken = false;
    [SyncVar] public bool Character2Taken = false;
    [SyncVar] public bool Character3Taken = false;
    [SyncVar] public bool Character4Taken = false;
    [SyncVar] public int numCharactersChoosen = 0;
    [SyncVar] private bool startMatch = false;
    [SyncVar] private bool matchIsStarting = false;
    
    //Local network variables
    private bool Character1TakenLocal = false;
    private bool Character2TakenLocal = false;
    private bool Character3TakenLocal = false;
    private bool Character4TakenLocal = false;

    //Scene management
    private SceneLoader.SceneLoader _sceneLoader;
    private NetworkingManager.NetworkingManager _networkingManager;
    
    private void Start() {
        //UI SETTINGS
        MessageBox.SetActive(false);
        
        //NETWORK and SCENE settings
        _sceneLoader = FindObjectOfType<SceneLoader.SceneLoader>();
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
       
    }
    private void Update()
    {
        UpdateNetworkVariables();
        if (isServer && _networkingManager.numPlayers == numCharactersChoosen)
        {
            StartCoroutine(countdownStart());
            matchIsStarting = true;
        }

        if (matchIsStarting)
        {
            MessageBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Starting match!";
            MessageBox.SetActive(true);
        }
        if (startMatch)
        {
            loadLobby();
        }
    }
    

    //----------------------------------------------------------UI VISUALS-----------------------------------------------------------------------------
    //Make character name not selectable
    private void setCharacterActive(Button button, bool value){
        button.GetComponent<Image>().color = !value ? Color.red : Color.clear;
        button.interactable = value ? !value : value;
    }
    
    //SHOW UI box "Character already choosen"
    void showCharacterAlreadyChoosen()
    {
        MessageBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Character already choosen!";
        MessageBox.SetActive(true);
        StartCoroutine(countdownDisappereance());
    }

    //SHOW UI box "Waiting for players"
    void showWaitingPlayers()
    {
        GameObject[] arrayDescription = GameObject.FindGameObjectsWithTag("UICharactersDescription");
        for (int i = 0; i < arrayDescription.Length; i++)
        {
            arrayDescription[i].SetActive(false);
        }
        MessageBox.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Waiting for other players!";
        MessageBox.SetActive(true);
    }

    

    //------------------------------------------------------NETWORK MANAGEMENT-------------------------------------------------------------------------------
    //Sync local variables with network variables
    public void UpdateNetworkVariables()
    {
        if (Character1TakenLocal != Character1Taken)
        {
            Character1TakenLocal = Character1Taken;
            setCharacterActive(Character1, Character4TakenLocal);
        }
        if (Character2TakenLocal != Character2Taken)
        {
            Character2TakenLocal = Character2Taken;
            setCharacterActive(Character2, Character4TakenLocal);
        }
        if (Character3TakenLocal != Character3Taken)
        {
            Character3TakenLocal = Character3Taken;
            setCharacterActive(Character3, Character4TakenLocal);
        }
        if (Character4TakenLocal != Character4Taken)
        {
            Character4TakenLocal = Character4Taken;            
            setCharacterActive(Character4, Character4TakenLocal);
        }
        
    }
    //SET character selected on server, called from UI buttons on "ACCEPT" character
    public void SelectCharacter(string characterName)
    {
        if (isClient)
        {
            if ((Character1TakenLocal && characterName == "Character1") ||
                (Character2TakenLocal && characterName == "Character2") ||
                (Character3TakenLocal && characterName == "Character3") ||
                (Character4TakenLocal && characterName == "Character4"))
            {
                showCharacterAlreadyChoosen();
            }
            else
            {
                GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>().playerName = characterName;
                GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSelectedCharacter(characterName);
                showWaitingPlayers(); 
            }
            
        }
    }
    
    //-------------------------------------------------------SCENE MANAGEMENT-------------------------------------------------------------------------------
    //LOAD lobby scene
    private void loadLobby(){
        _networkingManager.serverChangeScene("Lobby");
    }
    
    //-----------------------------------------------------------TIMERS-------------------------------------------------------------------------------------
    //Timer for UI disappearance
    IEnumerator countdownDisappereance()
    {
        yield return new WaitForSeconds(2f);
        MessageBox.SetActive(false);
    }
    //Timer for match start
    IEnumerator countdownStart()
    {
        yield return new WaitForSeconds(2f);
        startMatch = true;
    }
    


}