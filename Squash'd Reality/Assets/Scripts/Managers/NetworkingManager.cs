using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace NetworkingManager {
    public class NetworkingManager : MonoBehaviour
    {

        private NetworkManager _networkManager;
        void Awake() {
            _networkManager = GetComponent<NetworkManager>();
        }
        public void createLobby(){
            _networkManager.StartHost();
            Debug.Log("NetworkingManager::createLobby - Creating lobby");
        }

        public void joinLobby(){
            _networkManager.StartClient();
            Debug.Log("NetworkingManager::joinLobby - Joining lobby!");
        }
    }
}
