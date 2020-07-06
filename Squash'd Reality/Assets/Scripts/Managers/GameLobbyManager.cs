using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobbyManager : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
        playerController.CmdSpawnPlayer();
        Debug.Log("PLAYER SPAWND");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
