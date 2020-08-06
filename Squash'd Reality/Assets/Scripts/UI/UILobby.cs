using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UILobby : NetworkBehaviour
{
    [SyncVar] private bool matchIsStarting = false;
    private bool lobbyStarted = false;
    private NetworkingManager.NetworkingManager _networkingManager;
    
    [SerializeField] private GameObject lobbyPanel;



    // Start is called before the first frame update
    void Start()
    {
        lobbyPanel.SetActive(true);
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (isServer && _networkingManager.numPlayers == players.Length)
        {
            matchIsStarting = true;
        }

        if (matchIsStarting && !lobbyStarted)
        {
            lobbyStarted = true;
            StartCoroutine(waitingTime());
        }
    }


    IEnumerator waitingTime()
    {
        yield return new WaitForSeconds(0.5f);
        lobbyPanel.SetActive(false);
    }
}
