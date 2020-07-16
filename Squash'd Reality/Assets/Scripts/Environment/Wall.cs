using UnityEngine;

public class Wall : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer.Equals("Grabbable")){
            other.gameObject.GetComponentInParent<Grabber>().removeGrab();
        }
    }
}