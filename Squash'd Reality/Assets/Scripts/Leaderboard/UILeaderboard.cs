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
    
    public SyncListInt MarkusNobelStats = new SyncListInt();
    public SyncListInt KenNoloStats = new SyncListInt();
    public SyncListInt KamBryllalStats = new SyncListInt();
    public SyncListInt RapahelNosunStats = new SyncListInt();

    private void Start()
    {
        if (isServer)
        {
            MarkusNobelStats.Insert(0,0);
            MarkusNobelStats.Insert(1,0);
            MarkusNobelStats.Insert(2,0);
            MarkusNobelStats.Insert(3,0);
            MarkusNobelStats.Insert(4,0);
            
            KenNoloStats.Insert(0,0);
            KenNoloStats.Insert(1,0);
            KenNoloStats.Insert(2,0);
            KenNoloStats.Insert(3,0);
            KenNoloStats.Insert(4,0);
            
            KamBryllalStats.Insert(0,0);
            KamBryllalStats.Insert(1,0);
            KamBryllalStats.Insert(2,0);
            KamBryllalStats.Insert(3,0);
            KamBryllalStats.Insert(4,0);
            
            RapahelNosunStats.Insert(0,0);
            RapahelNosunStats.Insert(1,0);
            RapahelNosunStats.Insert(2,0);
            RapahelNosunStats.Insert(3,0);
            RapahelNosunStats.Insert(4,0);


        }
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

        StartCoroutine(wait3(playerStats, playerController));



    }

    IEnumerator wait3(PlayerStats playerStats, PlayerController playerController)
    {
        yield return new WaitForSeconds(2f);
        int playerNumber = 0;
        playerNumber = GameObject.FindGameObjectsWithTag("Player").Length;

        if (calcDeathPrize(playerNumber))
        {
            playerStats.setBonusPrize("CAN YOU NOT DIE?");
        }else if (calcFriendlyKill(playerNumber))
        {
            playerStats.setBonusPrize("CAN YOU NOT KILL YOUR FRIENDS?");
        }else if (calcPowerUp(playerNumber))
        {
            playerStats.setBonusPrize("UNDER POWER-UP STEROIDS");
        }else if (calcCollectible(playerNumber))
        {
            playerStats.setBonusPrize("COLLECTIBLE DUDE");
        }
        else
        {
            playerStats.setBonusPrize("WIN SOMETHING PLS");
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

    private bool calcAntivirusKilled(int playerNumber)
    {
        if (playerMoveset.playerName == "Markus Nobel")
        {
            if(MarkusNobelStats[3]!=0 && MarkusNobelStats[3]>= (MarkusNobelStats[3] + KenNoloStats[3] + KamBryllalStats[3] + RapahelNosunStats[3]) /playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            if(MarkusNobelStats[3]!=0 && MarkusNobelStats[3]>= (MarkusNobelStats[3] + KenNoloStats[3] + KamBryllalStats[3] + RapahelNosunStats[3]) /playerNumber)
            {
                return true;
            }
            
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            
            if(MarkusNobelStats[3]!=0 && MarkusNobelStats[3]>= (MarkusNobelStats[3] + KenNoloStats[3] + KamBryllalStats[3] + RapahelNosunStats[3]) /playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            
            if(MarkusNobelStats[3]!=0 && MarkusNobelStats[3]>= (MarkusNobelStats[3] + KenNoloStats[3] + KamBryllalStats[3] + RapahelNosunStats[3]) /playerNumber)
            {
                return true;
            }
        }

        return false;
    }
    private bool calcCollectible(int playerNumber)
    {
        if (playerMoveset.playerName == "Markus Nobel")
        {
            if (MarkusNobelStats[2] != 0 && MarkusNobelStats[2] >=
                (MarkusNobelStats[2] + KenNoloStats[2] + KamBryllalStats[2] + RapahelNosunStats[2]) /
                playerNumber)
            {
                return true;
            }
            
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            if (KenNoloStats[2] != 0 && KenNoloStats[2] >=
                (MarkusNobelStats[2] + KenNoloStats[2] + KamBryllalStats[2] + RapahelNosunStats[2]) /
                playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            if (KamBryllalStats[2] != 0 && KamBryllalStats[2] >=
                (MarkusNobelStats[2] + KenNoloStats[2] + KamBryllalStats[2] + RapahelNosunStats[2]) /
                playerNumber)
            {
                return true;
            }
            
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            if (RapahelNosunStats[2] != 0 && RapahelNosunStats[2] >=
                (MarkusNobelStats[2] + KenNoloStats[2] + KamBryllalStats[2] + RapahelNosunStats[2]) /
                playerNumber)
            {
                return true;
            }
        }

        return false;
    }
    private bool calcPowerUp(int playerNumber)
    {
        if (playerMoveset.playerName == "Markus Nobel")
        {
            if (MarkusNobelStats[1] != 0 && MarkusNobelStats[1] >=
                (MarkusNobelStats[1] + KenNoloStats[1] + KamBryllalStats[1] + RapahelNosunStats[1]) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            if (KenNoloStats[1] != 0 && KenNoloStats[1] >=
                (MarkusNobelStats[1] + KenNoloStats[1] + KamBryllalStats[1] + RapahelNosunStats[1]) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            if (KamBryllalStats[1] != 0 && KamBryllalStats[1] >=
                (MarkusNobelStats[1] + KenNoloStats[1] + KamBryllalStats[1] + RapahelNosunStats[1]) / playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            if (RapahelNosunStats[1] != 0 && RapahelNosunStats[1] >=
                (MarkusNobelStats[1] + KenNoloStats[1] + KamBryllalStats[1] + RapahelNosunStats[1]) / playerNumber)
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
            if (MarkusNobelStats[0] != 0 && MarkusNobelStats[0] >=
                (MarkusNobelStats[0] + KenNoloStats[0] + KamBryllalStats[0] + RapahelNosunStats[0]) /
                playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Ken Nolo")
        {
            if (KenNoloStats[0] != 0 && KenNoloStats[0] >=
                (MarkusNobelStats[0] + KenNoloStats[0] + KamBryllalStats[0] + RapahelNosunStats[0])  /
                playerNumber)
            {
                return true;
            }
            
        }else if (playerMoveset.playerName == "Kam Brylla")
        {
            if (KamBryllalStats[0] != 0 && KamBryllalStats[0] >=
                (MarkusNobelStats[0] + KenNoloStats[0] + KamBryllalStats[0] + RapahelNosunStats[0]) /
                playerNumber)
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Raphael Nosun")
        {
            if (RapahelNosunStats[0] != 0 && RapahelNosunStats[0] >=
                (MarkusNobelStats[0] + KenNoloStats[0] + KamBryllalStats[0] + RapahelNosunStats[0]) /
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

