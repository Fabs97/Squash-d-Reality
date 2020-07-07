﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

namespace NetworkingManager {
    public class NetworkingManager : NetworkManager {
        private float nextRefreshTime;
        private SceneLoader.SceneLoader _sceneLoader;
        private LevelManager.LevelManager _levelManager;
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
            matchMaker.CreateMatch("default", 4, true, "", "", "", 0, 0, OnMatchCreated);
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
        }

        public void JoinMatch(MatchInfoSnapshot match) {
            if (matchMaker == null)
                StartMatchMaker();

            matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
        }

        private void HandleJoinedMatch(bool success, string extendedinfo, MatchInfo responsedata) {
            StartClient(responsedata);
        }
        
        public int numberOfPlayers(){
            return numPlayers;
        }

        public List<GameObject> prefabList()
        {
            return spawnPrefabs;
        }

        public void serverChangeScene(string sceneName)
        {
            // TODO: having problems with already spawned players? Destroy them here!
            base.ServerChangeScene(_levelManager.loadNewLevel(sceneName));
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            base.OnServerSceneChanged(sceneName);
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
