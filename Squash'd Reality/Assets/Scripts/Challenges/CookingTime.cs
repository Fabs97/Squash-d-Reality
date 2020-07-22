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

    public void insertedIngredientInCauldron(Ingredient ingredient) {
        if(!ingredient.name.Equals(activeIngredients[0].name)) {
            endChallenge(false);
        }
        changeActiveIngredientList(ingredient);
        insertedIngredients++;
        if(insertedIngredients == difficulty * difficultyMultiplier) endChallenge(true);
    }

    private void changeActiveIngredientList(Ingredient ingredient){
        if(activeIngredients.Contains(ingredient)){
            removeIngredient(ingredient);
            int count = spawnedIngredients.Count < maxActiveIngredients ? spawnedIngredients.Count : maxActiveIngredients;
            activeIngredients = spawnedIngredients.GetRange(0, count);
        } else {
            addToActiveList(ingredient);
        }
        GameObject.FindGameObjectWithTag("UICookingTime").gameObject.GetComponent<UICookingTime>().setImages(activeIngredients);
    }

    private void addToActiveList(Ingredient ingredient){
        if(activeIngredients.Count < maxActiveIngredients) activeIngredients.Add(ingredient);
    }

    private void removeIngredient(Ingredient ingredient){
        activeIngredients.Remove(ingredient);
        spawnedIngredients.Remove(ingredient);
    }

    protected override void setDifficulty() {
        try {
            setMatch();
            List<string> playersNames = _networkingManager.getPlayersNames();
            
            if(!playersNames.Contains("Raphael Nosun")) _spawner.removeZone(3);
            else if(!playersNames.Contains("Kam Brylla")) _spawner.removeZone(2);
            else if(!playersNames.Contains("Ken Nolo")) _spawner.removeZone(1);
            else if(!playersNames.Contains("Markus Nobel")) _spawner.removeZone(0);
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

    private void setMatch(){
        int objectsToSpawn = difficulty * difficultyMultiplier;
        float totalTime = minTotalTime/difficulty;
        float moreTime = minMoreTime/difficulty;

        _spawner.objectsToSpawnCount = objectsToSpawn;
        _spawner.setSpawningDelay(totalTime / objectsToSpawn); 

        base._matchManager.setTimer(totalTime + moreTime);
    }

    public override void endChallenge(bool successful){
        _spawner.StopSpawning();
        base.endChallenge(successful);
    }
}