using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Ingredient : NetworkBehaviour {

    [SerializeField] public string name;
    private CookingTime _cookingTimeManager;
    public Texture2D image;
    [SyncVar] public bool isDouble;
    private void Start() {
        _cookingTimeManager = Object.FindObjectOfType<CookingTime>();
        StartCoroutine(wait());

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Cauldron") {
            _cookingTimeManager.insertedIngredientInCauldron(this, transform.gameObject.GetComponent<GrabbableMovementCookingTime>().grabbedBy);
            Destroy(gameObject, 3f);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        if (!isDouble)
        {
            _cookingTimeManager.addToSpawnedList(this);
        }
    }
}