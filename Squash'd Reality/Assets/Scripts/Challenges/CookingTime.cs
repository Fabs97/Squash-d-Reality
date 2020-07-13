using System;
using System.Collections.Generic;
using UnityEngine;

public class CookingTime : Challenge {

    private const int difficultyMultiplier = 8;
    private const int maxActiveIngredients = 2;
    private List<Ingredient> spawnedIngredients;
    private List<Ingredient> activeIngredients;

    private int insertedIngredients = 0;
    
    protected override void Start()
    {
        base.Start();
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
        try
        {
            Spawner spawner = FindObjectOfType<Spawner>();
            spawner.objectsToSpawnCount = difficulty * difficultyMultiplier;
            List<string> playersNames = _networkingManager.getPlayersNames();
            if(!playersNames.Contains("Raphael Nosun")) {
                Debug.Log("CookingTime::setDifficulty - Removing zone of Raphael Nosun!");
                spawner.removeZone(3);
            }
            if(!playersNames.Contains("Kam Brylla")) {
                Debug.Log("CookingTime::setDifficulty - Removing zone of Kam Brylla!");
                spawner.removeZone(2);
            }
            if(!playersNames.Contains("Ken Nolo")) {
                Debug.Log("CookingTime::setDifficulty - Removing zone of Ken Nolo!");
                spawner.removeZone(1);
            }
            if(!playersNames.Contains("Markus Nobel")) {
                Debug.Log("CookingTime::setDifficulty - Removing zone of Markus Nobel!");
                spawner.removeZone(0);
            }
            spawner.CmdStartSpawning();
            
        } catch (Exception e){
            Debug.LogError("CookingTime::setDifficulty - Catched Exception: " + e.StackTrace);
        }
        finally
        {
            base.setDifficulty();
        }
    }

    public override void endChallenge(bool successful){
        base.endChallenge(successful);
    }
}