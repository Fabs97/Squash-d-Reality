using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
  public Transform player;
  bool canBeGrabbed = false;
  bool grabbed = false;
  public float grabRange = 3.0f;

    // Update is called once per frame
    void Update()
    {
       Grab();
    }

    void Grab(){
         float dist = Vector3.Distance(gameObject.transform.position, player.position);

        if(dist <= grabRange){
            canBeGrabbed= true;
            Debug.Log("Geabbable::Grab - object can be grabbed");
        }
        else{
            canBeGrabbed = false;
        }

        if(canBeGrabbed && Input.GetButtonDown("Interact")){
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = player;
            grabbed = true;
            Debug.Log("Geabbable::Grab - got i down");
        }

        if(grabbed && Input.GetButtonUp("Interact")){
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            grabbed = false;
            Debug.Log("Geabbable::Grab - got i up");
        }
    }
}
