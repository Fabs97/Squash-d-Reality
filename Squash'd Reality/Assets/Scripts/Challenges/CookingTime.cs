using System.Collections.Generic;
using UnityEngine;

public class CookingTime : Challenge {

    private const int difficultyMultiplier = 8;
    private List<Ingredient> spawnedIngredients;
    private List<Ingredient> activeIngredients;

    private LevelManager.LevelManager _levelManager;
    private int difficulty;

    private int insertedIngredients = 0;
    private void Start()
    {
        spawnedIngredients = new List<Ingredient>();
        activeIngredients = new List<Ingredient>();
        _levelManager = Object.FindObjectOfType<LevelManager.LevelManager>();
        difficulty = _levelManager.getChallengeDifficulty();
    }

    public void addToSpawnedList(Ingredient ingredient){
        spawnedIngredients.Add(ingredient);
        changeActiveIngredientList(ingredient);
    }

    public void insertedIngredientInCauldron(Ingredient ingredient){
        if(!ingredient.Equals(activeIngredients[0])) {
            endChallenge(false);
        }
        changeActiveIngredientList(ingredient);
        insertedIngredients++;
        if(insertedIngredients == difficulty * difficultyMultiplier) endChallenge(true);
    }

    private void changeActiveIngredientList(Ingredient ingredient){
        if(activeIngredients.Contains(ingredient)){
            removeIngredient(ingredient);
            int count = spawnedIngredients.Count < 4 ? spawnedIngredients.Count : 4;
            activeIngredients = spawnedIngredients.GetRange(0, count);
        } else {
            addToActiveList(ingredient);
        }
        GameObject.FindGameObjectWithTag("UICookingTime").gameObject.GetComponent<UICookingTime>().setImages(activeIngredients);
    }

    private void addToActiveList(Ingredient ingredient){
        if(activeIngredients.Count < 4) activeIngredients.Add(ingredient);
    }

    private void removeIngredient(Ingredient ingredient){
        activeIngredients.Remove(ingredient);
        spawnedIngredients.Remove(ingredient);
    }

    protected override void setDifficulty() {
        Spawner spawner = Object.FindObjectOfType<Spawner>();
        spawner.objectsToSpawnCount = difficulty * difficultyMultiplier;
        spawner.startSpawning();
        base.setDifficulty();
    }

    protected override void endChallenge(bool successful){
        base.endChallenge(successful);
    }
}