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
        int startEnd = 0;
        int connected = 0;
        foreach (GameObject pipe in pipes)
        {
            if (pipe.transform.name != "PipeLineStart" && pipe.transform.name != "PipeLineEnd")
            {
                if (pipe.GetComponent<Pipe>().firstOrEnd)
                {
                    startEnd++;
                }

                if (pipe.GetComponent<Pipe>().isConnected)
                {
                    connected++;
                }
            }
            
        }

        if (startEnd == 2 && connected == 3)
        {
            matchWon = true;
            endChallenge(true);
        }
    }
    public void checkLine(){
        RaycastHit firstStepHit = start.transform.GetComponentInChildren<Hole>().fireHoleRaycast();
        //colpisco il figlio
        if(firstStepHit.collider != null) firstStepHit.collider.gameObject.transform.parent.gameObject.GetComponent<Pipe>().checkNextStep();
    }

    public void addToFinalPath(GameObject node){
        Debug.Log("ElectroPipeline::addToFinalPath -- added node to the list!");
        pathToTheEnd.Add(node);
    }

    public bool alreadyChecked(GameObject node){
        return pathToTheEnd.Contains(node);
    }

    public void lightUpPath(){
        // If I'm here, it means that the whole path is correct (?)
        Debug.Log("ElectroPipeline::lightUpPath -- I've hit the end by following the chain. This is the chain:");
        foreach(var obj in pathToTheEnd){
            Debug.Log("ElectroPipeline::lightUpPath -- " + obj.name);
        }
        Debug.Log("ElectroPipeline::lightUpPath -- I've completed to print the chain");
    }
    public override void endChallenge(bool successful){
        base.endChallenge(successful);
    }
}