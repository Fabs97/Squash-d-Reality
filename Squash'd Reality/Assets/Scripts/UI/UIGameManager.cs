using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIGameManager : NetworkBehaviour
{
    //Gameobjects elements in UI
    [SerializeField] private GameObject alertBox;
    [SerializeField] private GameObject backgroundPanelPause;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject lobbyButton;
    
    private bool pauseActive = false;
    
    //Scene management
    private SceneLoader.SceneLoader _sceneLoader;  

    // Start is called before the first frame update
    void Start()
    {
       //Setting elements not visible
        alertBox.SetActive(false);
        
        backgroundPanelPause.SetActive(false);
        
        //Scene management
        _sceneLoader = Object.FindObjectOfType<SceneLoader.SceneLoader>();
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start") && !pauseActive)
        {
            pauseActive = true;
            backgroundPanelPause.SetActive(true);
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(mainMenuButton);
            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                lobbyButton.SetActive(false);
            }
            else
            {
                lobbyButton.SetActive(true);
            }
        }else if (Input.GetButtonDown("Start") && pauseActive)
        {
            pauseActive = false;
            backgroundPanelPause.SetActive(false);
        }
    }

    public void MainMenuPressed()
    {
        _sceneLoader.loadNextScene("MainMenu");
        if (isServer)
        {
            Object.FindObjectOfType<NetworkingManager.NetworkingManager>().StopHost();

        }
        else if(isClient)
        {
            Object.FindObjectOfType<NetworkingManager.NetworkingManager>().StopClient();
 
        }
    }

    public void LobbyMenuPressed()
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdLobby();
    }
    
    //-----------------------------------------------DISCONNECTION UI-------------------------------------------------------------------
    //Call this function to notify the clients that a player disconnected
    public void CharacterDisconnected(string characterName)
    {
        alertBox.GetComponentInChildren<TextMeshProUGUI>().text = characterName + " has been disconnected!";
        alertBox.SetActive(true);
        StartCoroutine(countdownDisappearance(alertBox));
    }

    IEnumerator countdownDisappearance(GameObject disappearObject)
    {
        yield return new WaitForSeconds(3f);
        disappearObject.SetActive(false);
    }
    
    //Call this function to notify disconnection from server
    public void ShowAlertBoxPlayerDisconnected()
    {
        alertBox.GetComponentInChildren<TextMeshProUGUI>().text = "You have been disconnected!";
        alertBox.SetActive(true);
        StartCoroutine(countdownMainMenu());
    }

    IEnumerator countdownMainMenu()
    {
        yield return new WaitForSeconds(3f);
        _sceneLoader.loadNextScene("MainMenu");
    }
}
