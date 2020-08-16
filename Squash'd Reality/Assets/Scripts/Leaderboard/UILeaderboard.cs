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
    
    [SerializeField] private GameObject Player1MVP;
    [SerializeField] private GameObject Player2MVP;
    [SerializeField] private GameObject Player3MVP;
    [SerializeField] private GameObject Player4MVP;

    [SerializeField] private GameObject backButton;
    
    PlayerMoveset playerMoveset;

    
    //SYNCVAR
    [SyncVar] public int MarkusNoblePoints;
    [SyncVar] public int MarkusNobleDeaths;
    [SyncVar] public int MarkusNobelFriendlyKill;
    [SyncVar] public int MarkusNobelPowerUp;
    [SyncVar] public int MarkusNobelCollectible;
    [SyncVar] public int MarkusNobelAntivirusKilled;
    [SyncVar] public string MarkusNobleBonusPrize;
    
    [SyncVar] public int KenNoloPoints;
    [SyncVar] public int KenNoloDeaths;
    [SyncVar] public int KenNoloFriendlyKill;
    [SyncVar] public int KenNoloPowerUp;
    [SyncVar] public int KenNoloCollectible;
    [SyncVar] public int KenNoloAntivirusKilled;
    [SyncVar] public string KenNoloBonusPrize;
    
    [SyncVar] public int KamBryllaPoints;
    [SyncVar] public int KamBryllaDeaths;
    [SyncVar] public int KamBryllaFriendlyKill;
    [SyncVar] public int KamBryllaPowerUp;
    [SyncVar] public int KamBryllaCollectible;
    [SyncVar] public int KamBryllaAntivirusKilled;
    [SyncVar] public string KamBryllaBonusPrize;
    
    [SyncVar] public int RaphaelNosunPoints;
    [SyncVar] public int RaphaelNosunDeaths;
    [SyncVar] public int RaphaelNosunFriendlyKill;
    [SyncVar] public int RaphaelNosunPowerUp;
    [SyncVar] public int RaphaelNosunCollectible;
    [SyncVar] public int RaphaelNosunAntivirusKilled;
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
        
        if (isClient && playerMoveset.playerName == "Markus Nobel" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetMarkusNobleStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Ken Nolo" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKenNoloStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Kam Brylla" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKamBryllaStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Raphael Nosun" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetRaphaelNosunStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }

        int playerNumber = 0;
        if (MarkusNobelEnabled)
        {
            playerNumber++;
        }
        if(KenNoloEnabled)
        {
            playerNumber++;
        }

        if (KamBryllaEnabled)
        {
            playerNumber++;
        }

        if (RaphaelNosunEnabled)
        {
            playerNumber++;
        }
        
        if (calcDeathPrize(playerNumber))
        {
            playerStats.setBonusPrize("CAN YOU NOT DIE?");
        }else if (calcFriendlyKill(playerNumber))
        {
            playerStats.setBonusPrize("CAN YOU NOT KILL YOUR FRIENDS?");
        }else if (calcPowerUp(playerNumber))
        {
            playerStats.setBonusPrize("UNDER POWER UP STEROIDS");
        }
        
        if (isClient && playerMoveset.playerName == "Markus Nobel" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetMarkusNobleStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Ken Nolo" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKenNoloStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Kam Brylla" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKamBryllaStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Raphael Nosun" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetRaphaelNosunStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.bonusPrize);
        }
        
        
      

        StartCoroutine(wait2());
    }

    
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(2f);
        showPlayers();
        calcMVP();

    }

    private bool calcPowerUp(int playerNumber)
    {
        if (playerMoveset.playerName == "Markus Nobel")
        {
            if (MarkusNobelPowerUp != 0 && MarkusNobelPowerUp >=
                (MarkusNobelPowerUp + KenNoloPowerUp + KamBryllaPowerUp + RaphaelNosunPowerUp) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            if (KenNoloPowerUp != 0 && KenNoloPowerUp >=
                (MarkusNobelPowerUp + KenNoloPowerUp + KamBryllaPowerUp + RaphaelNosunPowerUp) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            if (KamBryllaPowerUp != 0 && KamBryllaPowerUp >=
                (MarkusNobelPowerUp + KenNoloPowerUp + KamBryllaPowerUp + RaphaelNosunPowerUp) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            if (RaphaelNosunPowerUp != 0 && RaphaelNosunPowerUp >=
                (MarkusNobelPowerUp + KenNoloPowerUp + KamBryllaPowerUp + RaphaelNosunPowerUp) / playerNumber)
            {
                return true;
            }
            
        }

        return false;
        
    }
    private bool calcFriendlyKill(int playerNumber)
    {
        if (playerMoveset.playerName == "Markus Nobel")
        {
            if (MarkusNobelFriendlyKill != 0 && MarkusNobelFriendlyKill >=
                (MarkusNobelFriendlyKill + KenNoloFriendlyKill + KamBryllaFriendlyKill + RaphaelNosunFriendlyKill) /
                playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            if (KenNoloFriendlyKill != 0 && KenNoloFriendlyKill >=
                (MarkusNobelFriendlyKill + KenNoloFriendlyKill + KamBryllaFriendlyKill + RaphaelNosunFriendlyKill) /
                playerNumber)
            {
                return true;
            }
            
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            if (KamBryllaFriendlyKill != 0 && KamBryllaFriendlyKill >=
                (MarkusNobelFriendlyKill + KenNoloFriendlyKill + KamBryllaFriendlyKill + RaphaelNosunFriendlyKill) /
                playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            if (RaphaelNosunFriendlyKill != 0 && RaphaelNosunFriendlyKill >=
                (MarkusNobelFriendlyKill + KenNoloFriendlyKill + KamBryllaFriendlyKill + RaphaelNosunFriendlyKill) /
                playerNumber)
            {
                return true;
            }
        }

        return false;  
    }

    private bool vuoto()
    {
        if (playerMoveset.playerName == "Markus Nobel")
        {
            
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            
        }

        return false;
    }
    private bool calcDeathPrize(int playerNumber)
    {
        if (playerMoveset.playerName == "Markus Nobel")
        {
            if (MarkusNobleDeaths!=0 && MarkusNobleDeaths >= (MarkusNobleDeaths + KenNoloDeaths + KamBryllaDeaths + RaphaelNosunDeaths) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            if (KenNoloDeaths!=0 && KenNoloDeaths >= (MarkusNobleDeaths + KenNoloDeaths + KamBryllaDeaths + RaphaelNosunDeaths) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            if (KamBryllaDeaths!= 0 && KamBryllaDeaths >= (MarkusNobleDeaths + KenNoloDeaths + KamBryllaDeaths + RaphaelNosunDeaths) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            if (RaphaelNosunDeaths!=0 && RaphaelNosunDeaths >= (MarkusNobleDeaths + KenNoloDeaths + KamBryllaDeaths + RaphaelNosunDeaths) / playerNumber)
            {
                return true;
            }
        }

        return false;
    }
    private void calcMVP()
    {
        //CALC MVP
        int maxPoints = 0;
        

        if (MarkusNobelEnabled && MarkusNoblePoints >= maxPoints)
        {
            maxPoints = MarkusNoblePoints;
        }

        if (KenNoloEnabled && KenNoloPoints >= maxPoints)
        {
            maxPoints = KenNoloPoints;
        }

        if (KamBryllaEnabled && KamBryllaPoints >= maxPoints)
        {
            maxPoints = KamBryllaPoints;
        }

        if (RaphaelNosunEnabled && RaphaelNosunPoints >= maxPoints)
        {
            maxPoints = RaphaelNosunPoints;
        }

        
        //SET MVP
        if (maxPoints == MarkusNoblePoints)
        {
            Player1MVP.SetActive(true);
        } 
        if (maxPoints == KenNoloPoints)
        {
            Player2MVP.SetActive(true);
        }
        
        if (maxPoints == KamBryllaPoints)
        {
            Player3MVP.SetActive(true);
        }
        
        if (maxPoints == RaphaelNosunPoints)
        {
            Player4MVP.SetActive(true);
        }
        
    }
     private void showPlayers()
    {
        backButton.SetActive(true);
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


     public void backToLobby()
     {
         NetworkingManager.NetworkingManager _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
         _networkingManager.serverChangeScene("Lobby", 0);
     }
}

