using UnityEngine;
using NetworkingManager;
using SceneLoader;
using UnityEngine.SceneManagement;

public class MainMenuButtonManager : MonoBehaviour
{
    private SceneLoader.SceneLoader _sceneLoader;
    private NetworkingManager.NetworkingManager _networkingManager;
    void Start() {
        _networkingManager = Object.FindObjectOfType<NetworkingManager.NetworkingManager>();
        _sceneLoader = Object.FindObjectOfType<SceneLoader.SceneLoader>();
    }
 
    public void playButtonClicked(){
        if (_sceneLoader == null)
        {
            Debug.LogError("PROBLEMA");
        }
        _sceneLoader.loadNextScene("LobbySelection");

    }

    public void createLobbyButtonClicked(){
        _networkingManager.StartHosting();
    }

    public void joinLobbyButtonClicked(){
        // _networkingManager.StartHosting();
    }

    public void CollectiblesMenuClicked()
    {
        SceneManager.LoadScene("CollectiblesMenu", LoadSceneMode.Single);
    }
    

}
