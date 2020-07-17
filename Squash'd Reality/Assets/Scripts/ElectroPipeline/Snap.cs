using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public float roomX;
    public float roomZ;

    private bool wasGrabbed = false;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(transform.parent != null) {
            Debug.Log("Snap::Update -- parent" + transform.parent.name);
            wasGrabbed = true;
        } 
        else if(wasGrabbed) {
            float x = Mathf.Round((transform.position.x * roomX) / roomX);
            float y = transform.position.y;
            float z = Mathf.Round((transform.position.z * roomZ) / roomZ);
            transform.position = new Vector3(x, y, z);
            transform.rotation = Quaternion.identity;
            GetComponent<Rigidbody>().isKinematic = true;
            wasGrabbed = false;
        }
    }
}
