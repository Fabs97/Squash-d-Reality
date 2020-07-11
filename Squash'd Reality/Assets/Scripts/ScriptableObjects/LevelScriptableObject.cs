using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelScriptableObject : ScriptableObject {
    [SerializeField] public string sceneName;
    [SerializeField] public bool spawnPlayers;
    private string[] playersNames = new string[] {"Character1", "Character2", "Character3", "Character4"};
    [SerializeField] public Vector3[] playersPositions;

    [SerializeField] public bool isDark = false;

    [SerializeField] public bool isChallenge = false;

    public Vector3 getPlayerPosition(string name){
        if(!spawnPlayers) return Vector3.one;
        
        for(int i = 0; i < playersNames.Length; i++) {
            if(playersNames[i] == name) return playersPositions[i];
        }
        return Vector3.one;
    }
}