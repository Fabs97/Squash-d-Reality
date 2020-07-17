using UnityEngine;

public class Hole : MonoBehaviour {
    int layerMask = 1 << 30;
    private const float raycastDistance = 0.3f;
    
    public void checkHoleConnection(){
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.right, out raycastHit, raycastDistance, layerMask)){
            Debug.Log("Hole::checkHoleConnection - hit gameObject with name: " + raycastHit.collider.gameObject.name);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.right);
    }
}