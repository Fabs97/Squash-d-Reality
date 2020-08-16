using UnityEngine;
using UnityEngine.Networking;

public class Ingredient : NetworkBehaviour {

    [SerializeField] public string name;
    private CookingTime _cookingTimeManager;
    public Texture2D image; 
    private void Start() {
        _cookingTimeManager = Object.FindObjectOfType<CookingTime>();
        _cookingTimeManager.addToSpawnedList(this);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Cauldron") {
            _cookingTimeManager.insertedIngredientInCauldron(this, transform.gameObject.GetComponent<GrabbableMovementCookingTime>().grabbedBy);
            Destroy(gameObject, 3f);
        }
    }
}