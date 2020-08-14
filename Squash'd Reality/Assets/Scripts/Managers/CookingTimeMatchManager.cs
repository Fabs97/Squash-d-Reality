using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CookingTimeMatchManager : MatchManager
{
    [SyncVar] public bool matchFailed;
    
    protected override void Start()
    {
        base.Start();
        if (isServer)
        {
            matchFailed = false;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (matchFailed)
        {
            timeEnded();
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
        GameObject.FindObjectOfType<CookingTime>().endChallenge(false);
    }
}
