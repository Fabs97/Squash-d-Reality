using UnityEngine;
using NetworkingManager;
using SceneLoader;
public class MainMenuButtonManager : MonoBehaviour
{
    private SceneLoader.SceneLoader _sceneLoader;
    private NetworkingManager.NetworkingManager _networkingManager;
    void Start() {
        _networkingManager = Object.FindObjectOfType<NetworkingManager.NetworkingManager>();
        _sceneLoader = Object.FindObjectOfType<SceneLoader.SceneLoader>();
    }
 
    public void playButtonClicked(){
        _sceneLoader.loadNextScene("LobbySelection");
    }

    public void createLobbyButtonClicked(){
        _networkingManager.StartHosting();
    }

    public void joinLobbyButtonClicked(){
        // _networkingManager.StartHosting();
    }
}
