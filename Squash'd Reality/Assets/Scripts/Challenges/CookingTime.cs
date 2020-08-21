using System;
using System.Collections.Generic;
using UnityEngine;

public class CookingTime : Challenge {
    private const int difficultyMultiplier = 8;
    private const int maxActiveIngredients = 2;
    [SerializeField] private const float minTotalTime = 120f;
    [SerializeField] private const float minMoreTime = 30f;
    private List<Ingredient> spawnedIngredients;
    private List<Ingredient> activeIngredients;
    private Spawner _spawner;
    private int insertedIngredients = 0;

    [SerializeField] private GameObject deathzone;
    [SerializeField] private GameObject cauldron;
    [SerializeField] private GameObject DoorPlatform;
    
    
    protected override void Start()
    {
        base.Start();
        _spawner = FindObjectOfType<Spawner>();
        spawnedIngredients = new List<Ingredient>();
        activeIngredients = new List<Ingredient>();
        difficulty = _levelManager.getChallengeDifficulty();
        setDifficulty();
    }

    public void addToSpawnedList(Ingredient ingredient){
        spawnedIngredients.Add(ingredient);
        changeActiveIngredientList(ingredient);
    }

    public void insertedIngredientInCauldron(Ingredient ingredient, string playerName)
    {
        Debug.LogError("INSERTED: " + ingredient);
        Debug.LogError("FIRST IN LIST: " + activeIngredients[0].name);
        Debug.LogError("INSERTED By: " + playerName);
        int numPlayers = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<CookingTimeMatchManager>().numPlayers;
        int objectToCook = ((difficulty * difficultyMultiplier)/2)*numPlayers;
        GameObject localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer");
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("DDOL").GetComponent<PlayerStats>();
        if(!ingredient.name.Equals(activeIngredients[0].name))
        { 
            Debug.LogError("PLAYER STATS NAME: " + playerStats.playerName);
            Debug.LogError("PLAYER NAME: " + playerName);
            if (playerStats.playerName == playerName)
            {
                Debug.LogError("NON ORDINATO");
                playerStats.notOrdered++;
            }

            if (ingredient.isServer)
            {
                localPlayer.GetComponent<PlayerController>()
                    .CmdSetMatchFailedCookingTime(GameObject.FindGameObjectWithTag("MatchManager"), true);  
            }
            
        }else
        
        {
            Debug.LogError("PLAYER STATS NAME: " + playerStats.playerName);
            Debug.LogError("PLAYER NAME: " + playerName);
            if (playerStats.playerName == playerName)
            {
                Debug.LogError("ORDINATO");
                playerStats.greetChef++;
            }  
            
            removeIngredient(ingredient);
            int count = spawnedIngredients.Count < maxActiveIngredients ? spawnedIngredients.Count : maxActiveIngredients;
            activeIngredients = spawnedIngredients.GetRange(0, count);
            GameObject.FindGameObjectWithTag("UICookingTime").gameObject.GetComponent<UICookingTime>().setImages(activeIngredients);
            insertedIngredients++;
        }

        if(objectToCook == insertedIngredients) endChallenge(true);

    }

    private void changeActiveIngredientList(Ingredient ingredient){
        if(activeIngredients.Contains(ingredient)){
            removeIngredient(ingredient);
            int count = spawnedIngredients.Count < maxActiveIngredients ? spawnedIngredients.Count : maxActiveIngredients;
            activeIngredients = spawnedIngredients.GetRange(0, count);
        } else {
            addToActiveList(ingredient);
        }
        if(activeIngredients.Count == 0) endChallenge(true);
        GameObject.FindGameObjectWithTag("UICookingTime").gameObject.GetComponent<UICookingTime>().setImages(activeIngredients);
    }

    private void addToActiveList(Ingredient ingredient){
        if(activeIngredients.Count < maxActiveIngredients) activeIngredients.Add(ingredient);
    }

    private void removeIngredient(Ingredient ingredient)
    {
        activeIngredients.RemoveAt(0);
        spawnedIngredients.RemoveAt(0);
    }

    protected override void setDifficulty() {
        try {
            setMatch();
            List<string> playersNames = _networkingManager.getPlayersNames();
            
            if(!playersNames.Contains("Raphael Nosun")) _spawner.removeZone(3);
            if(!playersNames.Contains("Kam Brylla")) _spawner.removeZone(2);
            if(!playersNames.Contains("Ken Nolo")) _spawner.removeZone(1);
            if(!playersNames.Contains("Markus Nobel")) _spawner.removeZone(0);
            _spawner.CmdStartSpawning();
            
        } catch (Exception e) {
            // This try catch has been done because this setting must be done
            // server only, but this object does not need a Network Identity!
            // The thrown exception regarding the playersNames is correct as it
            // is only something required server-side
            Debug.LogWarning("CookingTime::setDifficulty - Catched Exception: " + e.StackTrace);
        }
        finally {
            base.setDifficulty();
        }
    }

    private void setMatch()
    {

        float totalTime = minTotalTime/difficulty;
        float moreTime = minMoreTime/difficulty;
        base._matchManager.setTimer(totalTime + moreTime);

        
        int numPlayer = _networkingManager.getPlayersNames().Count;
        Debug.LogError("DIFFICULTY: " + difficulty);
        Debug.LogError("DIFFICULTY MULTIPLIER: " + difficultyMultiplier);
        Debug.LogError("NUM PLAYERS: " + numPlayer);
        int objectsToSpawn = ((difficulty * difficultyMultiplier)/2)*numPlayer;
        Debug.LogError("OBJECTS TO SPAWN: " + objectsToSpawn);

        _spawner.objectsToSpawnCount = objectsToSpawn;
        _spawner.setSpawningDelay(((totalTime + moreTime - 14f) / objectsToSpawn) ); 
        
        _spawner.setTimeStopSpawning(7f);
    }

    public override void endChallenge(bool successful){
        _spawner.StopSpawning();
        base.endChallenge(successful);
        if (successful)
        {
            deathzone.SetActive(false);
            cauldron.SetActive(false);
            DoorPlatform.SetActive(true);
        }
    }
}