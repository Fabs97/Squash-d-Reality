using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameLobbyManager : NetworkBehaviour
{
    [SerializeField] private GameObject grabbableObject;
    void Start()
    {
        if (isServer)
        {
            //GameObject go = Instantiate(grabbableObject, grabbableObject.transform);
            //NetworkServer.Spawn(go);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
