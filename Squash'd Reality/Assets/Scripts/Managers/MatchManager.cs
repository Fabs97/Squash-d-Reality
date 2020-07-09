using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchManager : NetworkBehaviour
{
    private GameObject UIManager; 
    [SyncVar] bool gameReady;
    private bool matchStarting = false;
    private NetworkingManager.NetworkingManager _networkingManager;

    // Start is called before the first frame update
    void Start()
    {
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        UIManager = GameObject.FindWithTag("UIManager");
        UIManager.GetComponent<UIManager>().setInfoBoxText("Survive the enemies!");
        UIManager.GetComponent<UIManager>().setInfoBoxActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer && _networkingManager.numPlayers == GameObject.FindGameObjectsWithTag("Player").Length)
        {
            gameReady = true;
        }
        if (gameReady && !matchStarting)
        {
            matchStarting = true;
            UIManager.GetComponent<UIManager>().StartMatch(4f);
            StartCoroutine(matchStart());
        }
    }

    IEnumerator matchStart()
    {
        yield return new WaitForSeconds(5f);
        UIManager.GetComponent<UIManager>().setInfoBoxActive(false);
        if (isServer)
        {
            //START ROOM LOGIC
        }
        showPlayerUI();
    }

    private void showPlayerUI()
    {
        Debug.Log("PLAYER NAME: " + GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>().playerName);
        UIManager.GetComponent<UIManager>().setPlayerName(GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>().playerName);
        UIManager.GetComponent<UIManager>().showUIPlayer(true);
        //TODO: insert UI settings
    }
}
