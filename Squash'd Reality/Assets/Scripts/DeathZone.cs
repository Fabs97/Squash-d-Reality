using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.LogError("ENTRO TRIGGER");
        if(other.gameObject.tag.Equals("Player"))
        {
            PlayerMoveset playerMoveset = other.gameObject.GetComponent<PlayerMoveset>();
            playerMoveset.Die(playerMoveset.playerName);
            StartCoroutine(resetChallenge());
        }
    }

    IEnumerator resetChallenge()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<Challenge>().endChallenge(false);
    }
}
