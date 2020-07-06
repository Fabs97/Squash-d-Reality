using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public Transform player;
    bool canBeGrabbed = false;
    bool grabbed = false;
    public float grabRange = 3.0f;
    private float initY;

    private IEnumerator grabCooldown;
    bool inCooldown = false;

    private void Start()
    {
        initY = transform.position.y;
    }

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

        if (!grabbed && transform.position.y != initY) {
            inCooldown = true;
            initY = transform.position.y;
            grabCooldown = WaitCooldown(2);
            StartCoroutine(grabCooldown);
        }

        if(canBeGrabbed && Input.GetButtonDown("Interact") && !inCooldown){
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = player;
            grabbed = true;
            if (transform.position.y < initY + 0.5f)
            {
                transform.position += new Vector3(0, 1f, 0);
            }
            Debug.Log("Geabbable::Grab - got i down");
        }

        if(grabbed && Input.GetButtonUp("Interact")){
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            grabbed = false;
            Debug.Log("Geabbable::Grab - got i up");
        }

    }

    private IEnumerator WaitCooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        inCooldown = false;
    }
}
