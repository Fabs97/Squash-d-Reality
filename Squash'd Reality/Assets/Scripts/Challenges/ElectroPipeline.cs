using System.Collections.Generic;
using UnityEngine;

public class ElectroPipeline : Challenge {
    
    private GameObject start;
    private List<GameObject> pathToTheEnd;
    protected override void Start(){
        base.Start();
        start = GameObject.Find("PipeLineStart");
        pathToTheEnd = new List<GameObject>();
        pathToTheEnd.Add(start);
    }

    public void checkLine(){
        RaycastHit firstStepHit = start.transform.GetComponentInChildren<Hole>().fireHoleRaycast();
        //colpisco il figlio
        firstStepHit.collider.gameObject.transform.parent.gameObject.GetComponent<Pipe>().checkNextStep();
    }

    public void addToFinalPath(GameObject node){
        Debug.Log("ElectroPipeline::addToFinalPath -- added node to the list!");
        pathToTheEnd.Add(node);
    }

    public bool alreadyChecked(GameObject node){
        return pathToTheEnd.Contains(node);
    }

    public void lightUpPath(){
        Debug.Log("ElectroPipeline::lightUpPath -- I've hit the end by following the chain. This is the chain:");
        foreach(var obj in pathToTheEnd){
            Debug.Log("ElectroPipeline::lightUpPath -- " + obj.name);
        }
        Debug.Log("ElectroPipeline::lightUpPath -- I've completed to print the chain");
    }
}