using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        triggerEnter(other);
    }

    public virtual void triggerEnter(Collider other) { }
}
