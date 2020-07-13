using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DarkPuzzleMatchManager : MatchManager
{
    [SyncVar] public bool light1On;
    [SyncVar] public bool light2On;
    [SyncVar] public bool light3On;
    [SyncVar] public bool light4On;

    protected override void Start()
    {
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0f;
        base.Start();
        
    }
    protected override void Update()
    {
        base.Update();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player.GetComponent<DummyMoveset>().playerName == "Markus Nobel")
            {
                player.GetComponent<Grabber>().toggleLight(light1On);
            }
            if (player.GetComponent<DummyMoveset>().playerName == "Ken Nolo")
            {
                player.GetComponent<Grabber>().toggleLight(light2On);
            }
            if (player.GetComponent<DummyMoveset>().playerName == "Kam Brylla")
            {
                player.GetComponent<Grabber>().toggleLight(light3On);
            }
            if (player.GetComponent<DummyMoveset>().playerName == "Raphael Nosun")
            {
                player.GetComponent<Grabber>().toggleLight(light4On);
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
        base.timeEnded();
    }

    protected override IEnumerator resetChallenge()
    {
        return base.resetChallenge();
    }
}
