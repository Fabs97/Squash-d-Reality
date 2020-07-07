using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelScriptableObject : ScriptableObject {
    [SerializeField] public string sceneName;
    [SerializeField] public bool spawnPlayers;
    private string[] playersNames = new string[] {"Ken Nolo", "Kam Brylla", "Markus Nobel", "Raphael Nosun"};
    [SerializeField] public Vector3[] playersPositions;

    public Vector3? getPlayerPosition(string name){
        if(!spawnPlayers) return null;
        
        for(int i = 0; i < playersNames.Length; i++) {
            if(playersNames[i] == name) return playersPositions[i];
        }
        return null;
    }
}