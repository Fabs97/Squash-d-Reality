using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

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
            Debug.Log("NetworkingManager::createLobby - networkAddress: " + _networkManager.networkAddress);
            Debug.Log("NetworkingManager::createLobby - networkPort: " + _networkManager.networkPort);
        }

        public void joinLobby(){
            _networkManager.StartClient();
            Debug.Log("NetworkingManager::joinLobby - Joining lobby!");
            Debug.Log("NetworkingManager::joinLobby - networkAddress: " + _networkManager.networkAddress);
            Debug.Log("NetworkingManager::joinLobby - networkPort: " + _networkManager.networkPort);
        }
    }

    public class HostGame : MonoBehaviour{
        List<MatchInfoSnapshot> matchList = new List<MatchInfoSnapshot>();
        bool matchCreated;
        NetworkMatch networkMatch;

        void Awake() {
            networkMatch = gameObject.AddComponent<NetworkMatch>();    
        }

        void OnGUI(){
            // You would normally not join a match you created yourself but this is possible here for demonstration purposes.
            if (GUILayout.Button("Create Room")) {
                string matchName = "room";
                uint matchSize = 4;
                bool matchAdvertise = true;
                string matchPassword = "";

                networkMatch.CreateMatch(matchName, matchSize, matchAdvertise, matchPassword, "", "", 0, 0, OnMatchCreate);
            }

            if (GUILayout.Button("List rooms")) {
                networkMatch.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
            }

            if (matchList.Count > 0) {
                GUILayout.Label("Current rooms");
            }
            foreach (var match in matchList) {
                if (GUILayout.Button(match.name)) {
                    networkMatch.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
                }
            }
        }
        private void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo){
            if(success) {
                Debug.Log("HostGame::OnMatchCreate - Create match succeeded");
                matchCreated = true;
                NetworkServer.Listen(matchInfo, 9000);
                Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
            } else {
                Debug.LogError("HostGame::OnMatchCreate - Create match failed: " + extendedInfo);
            }
        }

        private void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo) {
            if(success){
                Debug.Log("HostGame::OnMatchJoined - Joined match successfully");
                if(matchCreated){
                    Debug.LogWarning("HostGame::OnMatchJoined - Match already set up, ABORTING");
                    return;
                }
                Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
                NetworkClient myClient = new NetworkClient();
                myClient.RegisterHandler(MsgType.Connect, OnConnected);
                myClient.Connect(matchInfo);
            } else {
                Debug.LogError("HostGame::OnMatchJoined - Join match failed: " + extendedInfo);
            }
        }

        public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches) {
            if (success && matches != null && matches.Count > 0) {
                networkMatch.JoinMatch(matches[0].networkId, "", "", "", 0, 0, OnMatchJoined);
            }
            else if (!success) {
                Debug.LogError("List match failed: " + extendedInfo);
            }
        }

        private void OnConnected(NetworkMessage msg){
            Debug.Log("HostGame::OnConnected - Correctly connected: " + msg);
        }
    }
}
