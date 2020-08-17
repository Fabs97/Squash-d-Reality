using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CookingTimeMatchManager : MatchManager
{
    [SyncVar] public bool matchFailed;
    [SyncVar] public int numPlayers;
    private bool isFailed = false; 
    protected override void Start()
    {
        base.Start();
        NetworkingManager.NetworkingManager _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        if (isServer)
        {
            numPlayers = _networkingManager.getPlayersNames().Count;
            matchFailed = false;
        }
        isFailed = false;

    }

    protected override void Update()
    {
        base.Update();
        if (!isFailed && matchFailed)
        {
            isFailed = true;
            wrongFood(); 
        }
    }

    protected override IEnumerator matchStart()
    {
        return base.matchStart();
    }

    protected override void showPlayerUI()
    {
        base.showPlayerUI();
    }

    public void wrongFood()
    {
        base.timeEnded();
        _uiManager.setInfoBoxText("WRONG FOOD: YOU LOSE");
        _uiManager.setInfoBoxActive(true);
        StartCoroutine(resetChallenge());
        
    }
    public override void timeEnded()
    {
        if (!matchWon)
        {
            base.timeEnded();
            _uiManager.setInfoBoxText("TIME ENDED: YOU LOSE");
            _uiManager.setInfoBoxActive(true);
            if (isServer)
            {
                StartCoroutine(resetChallenge());
            }   
        }
        
    }

    protected override IEnumerator resetChallenge()
    {
        yield return new WaitForSeconds(2f);
        if (isServer)
        {
            FindObjectOfType<CookingTime>().endChallenge(false);
        }
        else
        {
            _uiManager.setInfoBoxText("YOU LOSE!");
        }
    }
    
}
