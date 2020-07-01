using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.loadNextScene("MainMenu");
    }

    public void loadNextScene(string sceneName){  
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    
}
