using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathZone : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Player"))
        {
            PlayerMoveset playerMoveset = other.gameObject.GetComponent<PlayerMoveset>();
            playerMoveset.Die(playerMoveset.playerName);
            StartCoroutine(resetChallenge());
        }
    }

    IEnumerator resetChallenge()
    {        
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        yield return new WaitForSeconds(2f);
        if (isServer)
        {
            FindObjectOfType<Challenge>().endChallenge(false);

        }
        else
        {
            uiManager.setInfoBoxText("YOU LOSE!");

        }
    }
}
