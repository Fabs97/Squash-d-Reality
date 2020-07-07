using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    //Gameobjects elements in UI
    [SerializeField] private GameObject alertBox;
    [SerializeField] private GameObject backgroundPanelPause;
    
    // Start is called before the first frame update
    void Start()
    {
       //Setting elements not visible
        alertBox.SetActive(false);
        backgroundPanelPause.SetActive(false);
    }
    
    
    //-----------------------------------------------DISCONNECTION UI-------------------------------------------------------------------
    //Call this function to notify the clients that a player disconnected
    public void CharacterDisconnected(string characterName)
    {
        alertBox.GetComponentInChildren<TextMeshProUGUI>().text = characterName + " has been disconnected!";
        alertBox.SetActive(true);
        StartCoroutine(countdownDisappearance());
    }

    IEnumerator countdownDisappearance()
    {
        yield return new WaitForSeconds(3f);
        alertBox.SetActive(false);
    }
    
    //Call this function to notify disconnection from server
    public void ShowAlertBoxPlayerDisconnected()
    {
        alertBox.GetComponentInChildren<TextMeshProUGUI>().text = "You have been disconnected!";
        
    }
}
