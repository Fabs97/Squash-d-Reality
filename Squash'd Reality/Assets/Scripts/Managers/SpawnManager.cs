using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace SpawnManager {
    public class SpawnManager : NetworkBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        [Command]
        public void CmdSpawnPlayer(GameObject playerPrefab, Transform spawnPoint) {
            GameObject go = Instantiate(playerPrefab, spawnPoint);
            NetworkServer.Spawn(go);
            Debug.Log("SpawnManager::CmdSpawnPlayer - Spawned my player!");
        }
    }
}