using UnityEngine;

public class Challenge : MonoBehaviour {

    protected int difficulty;

    protected virtual void setDifficulty() { }

    protected virtual void endChallenge(bool successful){ 
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if(successful){
            uiManager.setInfoBoxText("YOU WIN!");
        } else {
            uiManager.setInfoBoxText("YOU LOSE!");
        }
        uiManager.setInfoBoxActive(true);
    }
    
}