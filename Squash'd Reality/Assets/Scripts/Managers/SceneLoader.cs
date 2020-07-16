using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader {
    public class SceneLoader : MonoBehaviour
    {
        private LevelManager.LevelManager _levelManager;
        // Start is called before the first frame update
        void Awake()
        {
            _levelManager = Object.FindObjectOfType<LevelManager.LevelManager>();
            loadNextScene("MainMenu");
        }

        public void loadNextScene(string sceneName){
            string name = _levelManager.loadNewLevel(sceneName).sceneName;  
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
