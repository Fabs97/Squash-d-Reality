using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameManager : MonoBehaviour {
    
    private LevelManager.LevelManager _levelManager;
    private NetworkingManager.NetworkingManager _networkingManager;
    private int numPlayersInGame;
    
    private void Start() {
        _levelManager = Object.FindObjectOfType<LevelManager.LevelManager>();
        _networkingManager = Object.FindObjectOfType<NetworkingManager.NetworkingManager>();
        setupRoom();
        // TODO: load challenge script
        // _levelManager.getCurrentLevel().isChallenge
    }

    private void Update() {
        
    }

    private void setupRoom() {
        numPlayersInGame = _networkingManager.numPlayers;
    }

    public void calcNextDoor(int playersInDoor, string nextSceneName, int difficulty) {
        if(playersInDoor >= 2) _networkingManager.serverChangeScene(nextSceneName, difficulty);
    }

}