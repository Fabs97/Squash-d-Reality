using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Door : NetworkBehaviour {
    private SceneLoader.SceneLoader _sceneLoader;
    private NetworkGameManager _networkGameManager;
    private NetworkingManager.NetworkingManager _networkingManager;

    [SerializeField] public string nextSceneName;
    [SerializeField] private string doorName;
    [Range(1, 3)] [SerializeField] private int difficulty = 1; 
    private int playersInMe;
    private List<string> people = new List<string>();
    private bool stop = false;
    private void Start()
    {
        stop = false;
        TextMeshProUGUI nameDoor = transform.GetComponentInChildren<Canvas>().GetComponentInChildren<Button>()
            .GetComponentInChildren<TextMeshProUGUI>();
        nameDoor.text = doorName;
        _sceneLoader = Object.FindObjectOfType<SceneLoader.SceneLoader>();
        _networkGameManager = Object.FindObjectOfType<NetworkGameManager>();
        _networkingManager = Object.FindObjectOfType<NetworkingManager.NetworkingManager>();

    }

    private void Update()
    {
        if (!stop && people.Count == 2)
        {
            stop = true;
            if (!GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().isServer)
            {
                Debug.LogError("CAMBIO SCENA");
                _networkingManager.serverChangeScene(nextSceneName, difficulty);
            }
            else
            {
                Debug.LogError("CAMBIO SCENA 2");
                StartCoroutine(waitDelay());
            }
            
        }
    }
    

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            if (!people.Contains(other.gameObject.name))
            {
                people.Add(other.gameObject.name);
            }

            playersInMe = people.Count;
            //updatePeopleInDoor(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            if (people.Contains(other.gameObject.name))
            {
                people.Remove(other.gameObject.name);
            }
            playersInMe = people.Count;
          //  updatePeopleInDoor(false);
        }
    }

    private void updatePeopleInDoor(bool entered){

        if (GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().isServer)
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
        yield return new WaitForSeconds(1f);
        _networkGameManager.calcNextDoor(playersInMe, nextSceneName, difficulty);

    }
}