using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingManager : MonoBehaviour
{

    public static NetworkingManager instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
