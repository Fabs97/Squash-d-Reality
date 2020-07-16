using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManager{
    public class LevelManager : MonoBehaviour{
        private LevelScriptableObject currentLevel;
        private LevelScriptableObject[] levels;

        private int nextChallengeDifficulty;

        private void Awake() {
            loadLevels();

            currentLevel = getLevel("MainMenu"); 
        }
        
        public void loadLevels(){
            levels = Resources.LoadAll<LevelScriptableObject>("Levels");
        }

        public LevelScriptableObject loadNewLevel(string name) {
            currentLevel = getLevel(name);
            return currentLevel;
        }

        public int getChallengeDifficulty(){
            return nextChallengeDifficulty;
        }

        public LevelScriptableObject loadNewChallenge(string name, int difficulty){
            nextChallengeDifficulty = difficulty;
            return loadNewLevel(name);
        }

        public LevelScriptableObject getLevel(string name) {
            for(int i = 0; i < levels.Length; i++){
                if(levels[i].name == name) return levels[i];
            }
            return null;
        }

        public LevelScriptableObject getCurrentLevel(){
            return this.currentLevel;
        }
    }
}