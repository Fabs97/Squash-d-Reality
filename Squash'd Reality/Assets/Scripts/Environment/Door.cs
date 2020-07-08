using UnityEngine;
using UnityEngine.Networking;

public class Door : NetworkBehaviour {
    private SceneLoader.SceneLoader _sceneLoader;
    private NetworkGameManager _networkGameManager;
    
    [SerializeField] private string nextSceneName;
    private int playersInMe;
    // [Range(1, 3)] [SerializeField] private int difficulty = 1; 

    private void Start() {
        _sceneLoader = Object.FindObjectOfType<SceneLoader.SceneLoader>();
        _networkGameManager = Object.FindObjectOfType<NetworkGameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "LocalPlayer"){
            CmdUpdatePeopleInDoor(true);
            Debug.Log("Door::OnTriggerEnter -- Entered LocalPlayer in my area of effect");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "LocalPlayer"){
            CmdUpdatePeopleInDoor(false);
            Debug.Log("Door::OnTriggerExit -- Exited LocalPlayer in my area of effect");
        }
    }

    [Command]
    private void CmdUpdatePeopleInDoor(bool entered){
        playersInMe = playersInMe + (entered ? 1 : -1);
        _networkGameManager.calcNextDoor(playersInMe, nextSceneName);
    }
}