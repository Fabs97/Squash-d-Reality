using UnityEngine;

public class Challenge : MonoBehaviour {
    protected int difficulty;
    protected NetworkingManager.NetworkingManager _networkingManager;
    protected LevelManager.LevelManager _levelManager;

    protected virtual void Start() {
        _networkingManager = Object.FindObjectOfType<NetworkingManager.NetworkingManager>();
        _levelManager = Object.FindObjectOfType<LevelManager.LevelManager>();
    }

    protected virtual void setDifficulty() { }

    protected virtual void endChallenge(bool successful){ 
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if(successful){
            uiManager.setInfoBoxText("YOU WIN!");
        } else {
            uiManager.setInfoBoxText("YOU LOSE!");
            _networkingManager.serverChangeScene(_levelManager.getCurrentLevel().sceneName, difficulty);
        }
        uiManager.setInfoBoxActive(true);
    }
    
}