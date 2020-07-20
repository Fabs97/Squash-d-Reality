using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Collectibles : MonoBehaviour
{
   private Coroutine intelAcquisition;
   private Collider firstEntered = null;
   private bool triggerActivated = false;
   private bool logicCompleted = false;

   private void OnTriggerEnter(Collider other)
   {
      if (!triggerActivated)
      {
         triggerActivated = true;
         firstEntered = other;
         if (other.GetComponent<PlayerMoveset>().hasAuthority)
         {
            GameObject.FindGameObjectWithTag("UICollectibleManager").GetComponent<UICollectibleManager>().setWaitIntel(true);

         }
         intelAcquisition = StartCoroutine(waitIntel(other));
      }
      
   }

   private void Update()
   {
      if (triggerActivated && Vector3.Distance(gameObject.transform.position, firstEntered.transform.position)>=2f)
      {
         StopCoroutine(intelAcquisition);
         triggerActivated = false;
         
         GameObject.FindGameObjectWithTag("UICollectibleManager").GetComponent<UICollectibleManager>().setWaitIntel(false);
         GameObject.FindGameObjectWithTag("UICollectibleManager").GetComponent<UICollectibleManager>().setIntelAcquired(false);
      }
   }

   IEnumerator waitIntel(Collider other)
   {
      yield return new WaitForSeconds(3f);
      PlayerMoveset playerMoveset = other.GetComponent<PlayerMoveset>();
      if (playerMoveset.hasAuthority)
      {
         GameObject.FindGameObjectWithTag("UICollectibleManager").GetComponent<UICollectibleManager>().setWaitIntel(false);
         GameObject.FindGameObjectWithTag("UICollectibleManager").GetComponent<UICollectibleManager>().setIntelAcquired(true); 
      }
      playerMoveset.setCollectibleStats();
      PlayerPrefs.SetString(gameObject.name, "true");
      Destroy(this.gameObject);
   }
}
