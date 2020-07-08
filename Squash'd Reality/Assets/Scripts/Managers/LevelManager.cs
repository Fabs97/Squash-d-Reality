using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManager{
    public class LevelManager : MonoBehaviour{
        private LevelScriptableObject currentLevel;
        private LevelScriptableObject[] levels;

        private void Awake() {
            loadLevels();

            currentLevel = getLevel("MainMenu"); 
        }
        
        public void loadLevels(){
            levels = Resources.LoadAll<LevelScriptableObject>("Levels");
        }

        public string loadNewLevel(string name) {
            currentLevel = getLevel(name);
            return currentLevel.sceneName;
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