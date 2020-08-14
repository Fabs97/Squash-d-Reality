using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CookingTimeMatchManager : MatchManager
{
    [SyncVar] public bool matchFailed;
    private bool isFailed = false; 
    protected override void Start()
    {
        base.Start();
        if (isServer)
        {
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
        _uiManager.setInfoBoxText("WRONG FOOD: YOU DIED");
        _uiManager.setInfoBoxActive(true);
        StartCoroutine(resetChallenge());
        
    }
    public override void timeEnded()
    {
        base.timeEnded();
        _uiManager.setInfoBoxText("TIME ENDED: YOU DIED");
        _uiManager.setInfoBoxActive(true);
        if (isServer)
        {
            StartCoroutine(resetChallenge());
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
