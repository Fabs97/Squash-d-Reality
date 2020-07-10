using System.Collections.Generic;
using UnityEngine;

public class CookingTime : MonoBehaviour {
    private List<Ingredient> spawnedIngredients;
    private List<Ingredient> activeIngredients;

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
            activeIngredients = spawnedIngredients.GetRange(0, 4);
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
}