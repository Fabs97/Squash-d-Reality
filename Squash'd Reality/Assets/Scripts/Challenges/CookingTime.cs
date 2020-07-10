using System.Collections.Generic;
using UnityEngine;

public class CookingTime : Challenge {
    private List<Ingredient> spawnedIngredients;
    private List<Ingredient> activeIngredients;

    private void Start()
    {
        spawnedIngredients = new List<Ingredient>();
        activeIngredients = new List<Ingredient>();
    }

    public void addToSpawnedList(Ingredient ingredient){
        spawnedIngredients.Add(ingredient);
        changeActiveIngredientList(ingredient);
    }

    public void insertedIngredientInCauldron(Ingredient ingredient){
        changeActiveIngredientList(ingredient);
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
        spawner.objectsToSpawnCount = difficulty * 8;
        spawner.startSpawning();
        base.setDifficulty();
    }
}