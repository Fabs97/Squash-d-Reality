using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;

public class TrenchTimeMatchManager : MatchManager
{
    public bool matchTimeEnded = false;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isServer && _networkingManager.numPlayers == GameObject.FindGameObjectsWithTag("Player").Length)
        {
            gameReady = true;
        }
        if (gameReady && !matchStarting)
        {
            GameObject.FindObjectOfType<TrenchTime>().setPlayersConnected(GameObject.FindGameObjectsWithTag("Player").Length);
            matchStarting = true;
            UIManager.GetComponent<UIManager>().StartMatch(4f);
            StartCoroutine(matchStart());
        }

        if (matchTimeEnded)
        {
            GameObject.FindObjectOfType<TrenchTime>().timeEnded = true;
            if (isServer)
            {
                matchTimeEnded = false;
 
            }
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

    public override void timeEnded()
    {
        if (isServer)
        {
            GameObject.FindObjectOfType<Spawner>().StopSpawning();
        }
        matchTimeEnded = true;

    }

    protected override IEnumerator resetChallenge()
    {
        yield return new WaitForSeconds(2f);
        GameObject.FindObjectOfType<TrenchTime>().endChallenge(false);
    }

    
}
