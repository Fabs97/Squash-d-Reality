using System;
using System.Collections;
using System.Collections.Generic;
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

    private void showPlayers()
    {
        Player1.SetActive(MarkusNobelEnabled);
        Player2.SetActive(KenNoloEnabled);
        Player3.SetActive(KamBryllaEnabled);
        Player4.SetActive(RaphaelNosunEnabled);
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
}

