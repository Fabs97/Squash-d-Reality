using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchManager : NetworkBehaviour
{
    [SyncVar] protected bool gameReady;
    protected bool matchStarting = false;
    protected NetworkingManager.NetworkingManager _networkingManager;
    protected UIManager _uiManager;
    [SerializeField] private string openingString;
    [SerializeField] private float challengeTimer = 90f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        _uiManager.setInfoBoxText(openingString);
        _uiManager.setInfoBoxActive(true);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isServer && _networkingManager.numPlayers == GameObject.FindGameObjectsWithTag("Player").Length)
        {
            gameReady = true;
        }
        if (gameReady && !matchStarting)
        {
            matchStarting = true;
            _uiManager.StartMatch(4f);
            StartCoroutine(matchStart());
        }
        
    }

    protected virtual IEnumerator matchStart()
    {
        yield return new WaitForSeconds(5f);
        _uiManager.setInfoBoxActive(false);
        _uiManager.UIpanel.GetComponent<Image>().enabled = false;
        if (isServer)
        {
            //START ROOM LOGIC
        }
        showPlayerUI();
        _uiManager.StartCountdown(challengeTimer);
        
    }

    protected virtual void showPlayerUI()
    {
        string playerName = GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>().playerName;
        _uiManager.setPlayerImage(playerName);
        _uiManager.setPlayerName(playerName);
        _uiManager.showUIPlayer(true);
    }


    public virtual void timeEnded()
    {
        GameObject.FindWithTag("DDOL").GetComponent<PlayerStats>().timeOut++;
    }
    
    public void setTimer(float timer){
        challengeTimer = timer;
    }

    protected virtual IEnumerator resetChallenge()
    {
        yield return new WaitForSeconds(2f);
    }
}
