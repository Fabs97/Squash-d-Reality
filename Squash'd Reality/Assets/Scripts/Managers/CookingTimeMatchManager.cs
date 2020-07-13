using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingTimeMatchManager : MatchManager
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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
