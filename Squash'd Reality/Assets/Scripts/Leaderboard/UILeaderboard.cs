using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UILeaderboard : NetworkBehaviour
{
    //UI OBJECTS
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Player3;
    [SerializeField] private GameObject Player4;
    
    
    PlayerMoveset playerMoveset;

    
    //SYNCVAR
    [SyncVar] public int MarkusNoblePoints;
    [SyncVar] public int MarkusNobleDeaths;
    [SyncVar] public string MarkusNobleBonusPrize;
    
    [SyncVar] public int KenNoloPoints;
    [SyncVar] public int KenNoloDeaths;
    [SyncVar] public string KenNoloBonusPrize;
    
    [SyncVar] public int KamBryllaPoints;
    [SyncVar] public int KamBryllaDeaths;
    [SyncVar] public string KamBryllaBonusPrize;
    
    [SyncVar] public int RaphaelNosunPoints;
    [SyncVar] public int RaphaelNosunDeaths;
    [SyncVar] public string RaphaelNosunBonusPrize;
    
    [SyncVar] public bool MarkusNobelEnabled;
    [SyncVar] public bool KenNoloEnabled;
    [SyncVar] public bool KamBryllaEnabled;
    [SyncVar] public bool RaphaelNosunEnabled;
    
    private void Start()
    {
        StartCoroutine(wait());
    }
    

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);

        if (isServer)
        {
            MarkusNobelEnabled = false;
            KenNoloEnabled = false;
            KamBryllaEnabled = false;
            RaphaelNosunEnabled = false;
        }
        PlayerController playerController = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player.GetComponent<PlayerMoveset>().hasAuthority)
            {
                playerMoveset = player.GetComponent<PlayerMoveset>();

            }
        }
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("DDOL").GetComponent<PlayerStats>();
        playerStats.setTotalPoints();
        playerStats.setBonusPrize();
        
        if (isClient && playerMoveset.playerName == "Markus Nobel" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetMarkusNobleStats(playerStats.totalPoints, playerStats.death, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Ken Nolo" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKenNoloStats(playerStats.totalPoints, playerStats.death, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Kam Brylla" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKamBryllaStats(playerStats.totalPoints, playerStats.death, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Raphael Nosun" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetRaphaelNosunStats(playerStats.totalPoints, playerStats.death, playerStats.bonusPrize);
        }

        StartCoroutine(wait2());
    }

    IEnumerator wait2()
    {
        yield return new WaitForSeconds(2f);
        showPlayers();
    }
    
     private void showPlayers()
    {
        if (MarkusNobelEnabled)
        {
            for (int i = 0; i < Player1.transform.childCount; i++)
            {
                Transform button1 = Player1.transform.GetChild(i);
                if (button1.name == "PlayerPointsButton")
                {
                    button1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + MarkusNoblePoints;
                }

                if (button1.name == "PlayerDeathsButton")
                {
                    button1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + MarkusNobleDeaths;
                }
                if (button1.name == "PlayerBonusPrize")
                {
                    button1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + MarkusNobleBonusPrize;
                }
            }
        }
        Player1.SetActive(MarkusNobelEnabled);

        if (KenNoloEnabled)
        {
            for (int i = 0; i < Player2.transform.childCount; i++)
            {
                Transform button2 = Player2.transform.GetChild(i);
                if (button2.name == "PlayerPointsButton") {
                    button2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + KenNoloPoints;
                }
                else if (button2.name == "PlayerDeathsButton") {
                    button2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + KenNoloDeaths;
                }
                else if (button2.name == "PlayerBonusPrize"){
                    button2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + KenNoloBonusPrize;
                }
            }
        }
        Player2.SetActive(KenNoloEnabled);

        if (KamBryllaEnabled)
        {
            for (int i = 0; i < Player3.transform.childCount; i++)
            {
                Transform button3 = Player3.transform.GetChild(i);
                if (button3.name == "PlayerPointsButton") {
                    button3.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + KamBryllaPoints;
                }
                else if (button3.name == "PlayerDeathsButton") {
                    button3.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + KamBryllaDeaths;
                }
                else if (button3.name == "PlayerBonusPrize") {
                    button3.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + KamBryllaBonusPrize;
                }
            }
        }
        Player3.SetActive(KamBryllaEnabled);

        if (RaphaelNosunEnabled)
        {
            for (int i = 0; i < Player4.transform.childCount; i++)
            {
                Transform button4 = Player4.transform.GetChild(i);
                if (button4.name == "PlayerPointsButton") {
                    button4.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + RaphaelNosunPoints;
                }
                else if (button4.name == "PlayerDeathsButton") {
                    button4.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + RaphaelNosunDeaths;
                }
                else if (button4.name == "PlayerBonusPrize") {
                    button4.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + RaphaelNosunBonusPrize;
                }
            }
        }
        Player4.SetActive(RaphaelNosunEnabled);
    }
}

