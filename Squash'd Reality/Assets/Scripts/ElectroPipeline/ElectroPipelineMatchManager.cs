using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPipelineMatchManager : MatchManager
{
    public bool matchTimeEnded = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (matchTimeEnded)
        {
            FindObjectOfType<ElectroPipeline>().timeEnded = true;
            if (isServer) {
                matchTimeEnded = false;
            }
        }
        
    }

    protected override IEnumerator matchStart()
    {
        FindObjectOfType<Spawner>().CmdStartSpawning();
        return base.matchStart();
    }

    protected override void showPlayerUI()
    {
        base.showPlayerUI();
    }

    public override void timeEnded()
    {
        if (!matchWon)
        {
            base.timeEnded();
            if (isServer)
            {
                GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
                for (int i = 0; i < spawners.Length; i++)
                {
                    spawners[i].GetComponent<Spawner>().StopSpawning();
                }
            }
            matchTimeEnded = true; 
        }
        
    }

    protected override IEnumerator resetChallenge()
    {
        return base.resetChallenge();
    }
}
