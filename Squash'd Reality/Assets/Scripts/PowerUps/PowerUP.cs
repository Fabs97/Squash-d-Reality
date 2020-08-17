using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    protected virtual void Start()
    {
        StartCoroutine(powerUpDisappearing());
    }

    private void OnTriggerEnter(Collider other) {
        triggerEnter(other);
        
    }

    public virtual void triggerEnter(Collider other) { }

    IEnumerator powerUpDisappearing()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
