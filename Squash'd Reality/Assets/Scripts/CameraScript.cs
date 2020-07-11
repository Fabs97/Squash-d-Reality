using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraScript : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        if (!gameObject.GetComponentInParent<DummyMoveset>().hasAuthority)
        {
            this.gameObject.SetActive(false);
        }
    }
    
    
   
}
