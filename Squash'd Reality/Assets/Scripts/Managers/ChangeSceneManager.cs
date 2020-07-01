using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    public void loadNextScene(string sceneName){  
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
