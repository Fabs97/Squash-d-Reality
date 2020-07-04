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
    [SyncVar] public bool Character1Taken = false;
    [SyncVar] public bool Character2Taken = false;
    [SyncVar] public bool Character3Taken = false;
    [SyncVar] public bool Character4Taken = false;
    
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

    private void setCharacterActive(Button button, bool value){
        button.GetComponent<Image>().color = !value ? Color.red : Color.clear;
        button.interactable = value ? !value : value;
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


    public void SelectCharacter(string characterName)
    {
        if (isClient)
        {
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSelectedCharacter(characterName);
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