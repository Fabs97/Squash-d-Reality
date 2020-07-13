using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPuzzleMatchManager : MatchManager
{
    protected override void Start()
    {
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0f;
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
        base.timeEnded();
    }

    protected override IEnumerator resetChallenge()
    {
        return base.resetChallenge();
    }
}
