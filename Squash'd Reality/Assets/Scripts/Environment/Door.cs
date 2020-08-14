using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Door : NetworkBehaviour {
    private SceneLoader.SceneLoader _sceneLoader;
    private NetworkGameManager _networkGameManager;
    
    [SerializeField] private string nextSceneName;
    [SerializeField] private string doorName;
    [Range(1, 3)] [SerializeField] private int difficulty = 1; 
    private int playersInMe;

    private void Start()
    {
        TextMeshProUGUI nameDoor = transform.GetComponentInChildren<Canvas>().GetComponentInChildren<Button>()
            .GetComponentInChildren<TextMeshProUGUI>();
        nameDoor.text = doorName;
        _sceneLoader = Object.FindObjectOfType<SceneLoader.SceneLoader>();
        _networkGameManager = Object.FindObjectOfType<NetworkGameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            updatePeopleInDoor(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            updatePeopleInDoor(false);
        }
    }

    private void updatePeopleInDoor(bool entered){
        playersInMe = playersInMe + (entered ? 1 : -1);
        if (isServer)
        {
            StartCoroutine(waitDelay());
        }
        else
        {
            _networkGameManager.calcNextDoor(playersInMe, nextSceneName, difficulty);
        }
    }

    IEnumerator waitDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _networkGameManager.calcNextDoor(playersInMe, nextSceneName, difficulty);
    }
}