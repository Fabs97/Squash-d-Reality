using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<DummyMoveset>().Die(other.gameObject.GetComponent<DummyMoveset>().playerName);
            StartCoroutine(resetChallenge());
        }
    }

    IEnumerator resetChallenge()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<Challenge>().endChallenge(false);
    }
}
