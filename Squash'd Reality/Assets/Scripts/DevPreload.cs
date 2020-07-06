using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreload:MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(this);
        if (GameObject.Find("__app")==null) SceneManager.LoadScene("_preload"); 
    }
}