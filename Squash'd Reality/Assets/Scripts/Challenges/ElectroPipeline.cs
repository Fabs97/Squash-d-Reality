using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPipeline : Challenge {
    
    private GameObject start;
    private List<GameObject> pathToTheEnd;
    
    public bool timeEnded = false;
    private bool matchEnded = false;

    private bool matchWon = false;
    
    protected override void Start(){
        base.Start();
        start = GameObject.Find("PipeLineStart");
        pathToTheEnd = new List<GameObject>();
        pathToTheEnd.Add(start);
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipe in pipes)
        {
            if (pipe.transform.name != "PipeLineStart" && pipe.transform.name != "PipeLineEnd")
            {
                pipe.GetComponent<Pipe>().releasedPipe();
            }
            
        }

    }

    private void Update()
    {
        if (!matchEnded && timeEnded)
        {
            endChallenge(false);
        }

        if (!matchWon)
        {
            checkWin();
        }
    }

    public void checkWin()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        bool start = false;
        bool connectedEnd = false;
        foreach (GameObject pipe in pipes)
        {
            if (pipe.transform.name != "PipeLineStart" && pipe.transform.name != "PipeLineEnd")
            {
                if (pipe.GetComponent<Pipe>().isFirst)
                {
                    start = true;
                }

                if (pipe.GetComponent<Pipe>().isEnd && pipe.GetComponent<Pipe>().isConnected)
                {
                    connectedEnd = true;
                }
            }
            
        }

        if (start && connectedEnd)
        {
            matchWon = true;
            endChallenge(true);
        }
    }
    public override void endChallenge(bool successful){
        base.endChallenge(successful);
    }
}