using UnityEngine;

public class Challenge : MonoBehaviour {

    protected int difficulty;

    protected virtual void setDifficulty() { }

    protected virtual void endChallenge(bool successful){ 
        if(successful){
            GameObject.FindWithTag("UIManager").GetComponent<UIManager>().setInfoBoxText("YOU WIN!");
        } else {
            GameObject.FindWithTag("UIManager").GetComponent<UIManager>().setInfoBoxText("YOU LOSE!");
        }
        GameObject.FindWithTag("UIManager").GetComponent<UIManager>().setInfoBoxActive(true);
    }
    
}