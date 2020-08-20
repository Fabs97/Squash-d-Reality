using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMatchManager : MonoBehaviour
{
    private NetworkingManager.NetworkingManager _networkingManager;
    private UIManager _uiManager;
    private void Start()
    {
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        _uiManager.setInfoBoxActive(false);
        _uiManager.UIpanel.GetComponent<Image>().enabled = false;
        showPlayerUI();
    }
    
    private void showPlayerUI()
    {
        string playerName = GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>().playerName;
        _uiManager.setPlayerImage(playerName);
        _uiManager.setPlayerName(playerName);
        _uiManager.setPlayerElementsActive(true);
    }
}
