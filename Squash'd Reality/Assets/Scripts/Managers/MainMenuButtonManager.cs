using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    void Awake() {
        _sceneLoader = Object.FindObjectOfType<SceneLoader>();
    }

    public void playButtonClicked(){
        _sceneLoader.loadNextScene("LobbySelection");
    }

    public void joinLobbyButtonClicked(){
        Debug.Log("MainMenuButtonManager::joinLobbyButtonClicked - Joining Lobby");
    }

    public void createLobbyButtonClicked(){
        Debug.Log("MainMenuButtonManager::createLobbyButtonClicked - Creating Lobby");
    }
}
