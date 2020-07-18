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
    
    
    DummyMoveset dummyMoveset;

    
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
            if (player.GetComponent<DummyMoveset>().hasAuthority)
            {
                dummyMoveset = player.GetComponent<DummyMoveset>();

            }
        }
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("DDOL").GetComponent<PlayerStats>();
        
        if (isClient && dummyMoveset.playerName == "Markus Nobel" && dummyMoveset.hasAuthority)
        {
            playerController.CmdSetMarkusNobleStats(playerStats.totalPoints, playerStats.death, playerStats.bonusPrize);
        }else if (isClient && dummyMoveset.playerName == "Ken Nolo" && dummyMoveset.hasAuthority)
        {
            playerController.CmdSetKenNoloStats(playerStats.totalPoints, playerStats.death, playerStats.bonusPrize);
        }else if (isClient && dummyMoveset.playerName == "Kam Brylla" && dummyMoveset.hasAuthority)
        {
            playerController.CmdSetKamBryllaStats(playerStats.totalPoints, playerStats.death, playerStats.bonusPrize);
        }else if (isClient && dummyMoveset.playerName == "Raphael Nosun" && dummyMoveset.hasAuthority)
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
                if (Player1.transform.GetChild(i).name == "PlayerPointsButton")
                {
                    Player1.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + MarkusNoblePoints;
                }

                if (Player1.transform.GetChild(i).name == "PlayerDeathsButton")
                {
                    Player1.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + MarkusNobleDeaths;
                }
                if (Player1.transform.GetChild(i).name == "PlayerBonusPrize")
                {
                    Player1.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + MarkusNobleBonusPrize;
                }
            }
        }
        Player1.SetActive(MarkusNobelEnabled);

        if (KenNoloEnabled)
        {
            for (int i = 0; i < Player2.transform.childCount; i++)
            {
                if (Player2.transform.GetChild(i).name == "PlayerPointsButton")
                {
                    Player2.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + KenNoloPoints;
                }

                if (Player2.transform.GetChild(i).name == "PlayerDeathsButton")
                {
                    Player2.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + KenNoloDeaths;
                }
                if (Player2.transform.GetChild(i).name == "PlayerBonusPrize")
                {
                    Player2.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + KenNoloBonusPrize;
                }
            }
        }
        Player2.SetActive(KenNoloEnabled);

        if (KamBryllaEnabled)
        {
            for (int i = 0; i < Player3.transform.childCount; i++)
            {
                if (Player3.transform.GetChild(i).name == "PlayerPointsButton")
                {
                    Player3.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + KamBryllaPoints;
                }

                if (Player3.transform.GetChild(i).name == "PlayerDeathsButton")
                {
                    Player3.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + KamBryllaDeaths;
                }
                if (Player3.transform.GetChild(i).name == "PlayerBonusPrize")
                {
                    Player3.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + KamBryllaBonusPrize;
                }
            }
        }
        Player3.SetActive(KamBryllaEnabled);

        if (RaphaelNosunEnabled)
        {
            for (int i = 0; i < Player4.transform.childCount; i++)
            {
                if (Player4.transform.GetChild(i).name == "PlayerPointsButton")
                {
                    Player4.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + RaphaelNosunPoints;
                }

                if (Player4.transform.GetChild(i).name == "PlayerDeathsButton")
                {
                    Player4.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Deaths: " + RaphaelNosunDeaths;
                }
                if (Player4.transform.GetChild(i).name == "PlayerBonusPrize")
                {
                    Player4.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Bonus prize: " + RaphaelNosunBonusPrize;
                }
            }
        }
        Player4.SetActive(RaphaelNosunEnabled);
    }
}

