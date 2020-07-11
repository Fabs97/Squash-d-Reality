using UnityEngine;

public class Challenge : MonoBehaviour {

    protected int difficulty;

    protected virtual void setDifficulty() { }

    protected virtual void endChallenge(bool successful){ 
        if(successful){
            Debug.Log("You win!");
        } else {
            Debug.Log("You lose!");
        }
    }
    
}