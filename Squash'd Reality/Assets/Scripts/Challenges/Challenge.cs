using System.Collections;
using UnityEngine;
using System;

public class Challenge : MonoBehaviour {
    protected int difficulty;
    protected NetworkingManager.NetworkingManager _networkingManager;
    protected LevelManager.LevelManager _levelManager;

    protected virtual void Start() {
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        _levelManager = FindObjectOfType<LevelManager.LevelManager>();
    }

    protected virtual void setDifficulty() { }

    public virtual void endChallenge(bool successful){ 
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if(successful){
            uiManager.setInfoBoxText("YOU WIN!");
        } else {
            uiManager.setInfoBoxText("YOU LOSE!");
            StartCoroutine(waitToReset());
        }
        uiManager.setInfoBoxActive(true);
    }

    IEnumerator waitToReset()
    {
        yield return new WaitForSeconds(2f);
        _networkingManager.serverChangeScene(_levelManager.getCurrentLevel().sceneName, difficulty);

    }
    
}