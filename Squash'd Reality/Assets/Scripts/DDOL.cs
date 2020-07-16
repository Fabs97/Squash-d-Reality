using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    public string playerName = "default";
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
