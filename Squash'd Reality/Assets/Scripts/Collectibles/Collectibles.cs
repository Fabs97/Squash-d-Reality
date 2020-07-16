using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      PlayerPrefs.SetString(gameObject.name, "true");
   }
}
