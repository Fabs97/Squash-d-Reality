using UnityEngine;

public class DevPreload:MonoBehaviour {
    void Awake() {
        if (GameObject.Find("__app")==null) UnityEngine.SceneManagement.SceneManager.LoadScene("_preload"); 
    }
}