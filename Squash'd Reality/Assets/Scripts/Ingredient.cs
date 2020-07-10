using UnityEngine;

public class Ingredient : MonoBehaviour {

    private CookingTime _cookingTimeManager;
    [SerializeField] private Texture2D image; 
    private void Start() {
        _cookingTimeManager = Object.FindObjectOfType<CookingTime>();
        _cookingTimeManager.addToSpawnedList(this);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Cauldron") {
            _cookingTimeManager.insertedIngredientInCauldron(this);
        }
    }
}