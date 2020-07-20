using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;


namespace NetworkingManager {
    public class NetworkingManager : NetworkManager {
        private float nextRefreshTime;
        private SceneLoader.SceneLoader _sceneLoader;
        private LevelManager.LevelManager _levelManager;
        private List<string> _playersNames;
        public List<MatchInfoSnapshot> matches;
        public string currentMatchName;
        void Awake() {
            _sceneLoader = Object.FindObjectOfType<SceneLoader.SceneLoader>();
            _levelManager = Object.FindObjectOfType<LevelManager.LevelManager>();
        }
        public void createLobby(){
            base.StartHost();
            _sceneLoader.loadNextScene("CharactersSelection");

        }

        public void StartHosting() {
            StartMatchMaker();
            currentMatchName = "Match: " + Random.Range(0, 1000);
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].name == currentMatchName)
                {
                    currentMatchName = "Match: " + Random.Range(0, 1000);
                    i = -1;
                }
            }
            matchMaker.CreateMatch(currentMatchName, 4, true, "", "", "", 0, 0, OnMatchCreated);
        }

        private void OnMatchCreated(bool success, string extendedinfo, MatchInfo responsedata)
        {
            base.StartHost(responsedata);
        }

        private void Update()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName=="LobbySelection" && Time.time >= nextRefreshTime) {
                RefreshMatches();
            }
        }

        private void RefreshMatches() {
            nextRefreshTime = Time.time + 5f;

            if (matchMaker == null)
                StartMatchMaker();

            matchMaker.ListMatches(0, 10, "", true, 0, 0, HandleListMatchesComplete);
        }

        private void HandleListMatchesComplete(bool success, string extendedinfo, List<MatchInfoSnapshot> responsedata) {
            AvailableMatchesList.HandleNewMatchList(responsedata);
            matches = responsedata;
        }

        public void JoinMatch(MatchInfoSnapshot match) {
            if (matchMaker == null)
                StartMatchMaker();

            matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
        }

        private void HandleJoinedMatch(bool success, string extendedinfo, MatchInfo responsedata) {
            StartClient(responsedata);
        }

        public void serverChangeScene(string sceneName, int difficulty)
        {
            // TODO: having problems with already spawned players? Destroy them here!
            LevelScriptableObject nextLevel; 
            if(difficulty > 0) nextLevel = _levelManager.loadNewChallenge(sceneName, difficulty);
            else nextLevel = _levelManager.loadNewLevel(sceneName);
            base.ServerChangeScene(nextLevel.sceneName);
        }

        public void serverChangeScene(string sceneName){
            serverChangeScene(sceneName, 0);
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            base.OnServerSceneChanged(sceneName);
        }

        public override void OnClientDisconnect(NetworkConnection conn) {
            Debug.Log("NetworkingManager::OnClientDisconnect - I'm the client and I've been disconnected from server: " + conn.connectionId);
            FindObjectOfType<UIGameManager>().ShowAlertBoxPlayerDisconnected();
        }

        public override void OnServerDisconnect(NetworkConnection conn) {
            NetworkServer.DestroyPlayersForConnection(conn);
            if (conn.lastError != NetworkError.Ok) {
                if (LogFilter.logError) { Debug.LogError("NetworkingManager::OnServerDisconnect - ServerDisconnected due to error: " + conn.lastError); }
            }
            FindObjectOfType<UIGameManager>().CharacterDisconnected("A player");
            Debug.Log("NetworkingManager::OnServerDisconnect - A client disconnected from the server: " + conn);
        }

        public void addSelectedPlayer(string name){
            if(_playersNames == null) _playersNames = new List<string>();
            _playersNames.Add(name);
        }

        public List<string> getPlayersNames(){
            return this._playersNames;
        }
    }

	public static class AvailableMatchesList {
		public static event System.Action<List<MatchInfoSnapshot>> OnAvailableMatchesChanged = delegate { };

		private static List<MatchInfoSnapshot> matches = new List<MatchInfoSnapshot>();

		public static void HandleNewMatchList(List<MatchInfoSnapshot> matchList) {
			matches = matchList;
			OnAvailableMatchesChanged(matches);
		}
	}
}