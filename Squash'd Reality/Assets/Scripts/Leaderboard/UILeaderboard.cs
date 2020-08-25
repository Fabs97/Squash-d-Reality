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

    [SerializeField] private GameObject CheckValuesBox;

    [SerializeField] private GameObject backImage;
    
    PlayerMoveset playerMoveset;

    //SYNCVAR & SYNCLIST
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
    
    public SyncListFloat MarkusNobelStats = new SyncListFloat();
    public SyncListFloat KenNoloStats = new SyncListFloat();
    public SyncListFloat KamBryllalStats = new SyncListFloat();
    public SyncListFloat RapahelNosunStats = new SyncListFloat();

    //SYNCLIST VARIABLE ORDER INDEX
    /*
     * 0 --> friendlyKill
     * 1 --> powerUp
     * 2 --> collectible
     * 3 --> antivirusKilled
     * 4 --> notOrdered;
     * 5 --> greetChef
     * 6 --> cableManagement
     */
    private void Start()
    {
        if (isServer)
        {
            //SET DEFAULT VALUES
            for (int i = 0; i < 30; i++)
            {
                MarkusNobelStats.Insert(i, 0);
                KenNoloStats.Insert(i, 0);
                KamBryllalStats.Insert(i, 0);
                RapahelNosunStats.Insert(i, 0);
            }
        }
        StartCoroutine(wait());
    }
    

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        
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
        playerStats.setBonusPrize("");
        
        if (isClient && playerMoveset.playerName == "Markus Nobel" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetMarkusNobleStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef, playerStats.cableManagement,playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Ken Nolo" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKenNoloStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef,playerStats.cableManagement, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Kam Brylla" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKamBryllaStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef,playerStats.cableManagement, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Raphael Nosun" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetRaphaelNosunStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef,playerStats.cableManagement, playerStats.bonusPrize);
        }

        StartCoroutine(wait3(playerStats, playerController));
        

    }

    IEnumerator wait3(PlayerStats playerStats, PlayerController playerController)
    {
        yield return new WaitForSeconds(2f);
        int playerNumber = 0;
        playerNumber = GameObject.FindGameObjectsWithTag("Player").Length;
        
        if (calcPrizeByIndex(0, playerNumber))
        {
            playerStats.setBonusPrize("COULD YOU NOT KILL YOUR FRIENDS?");
        }else if (calcPrizeByIndex(1, playerNumber))
        {
            playerStats.setBonusPrize("UNDER POWER-UP STEROIDS");
        }else if (calcPrizeByIndex(2, playerNumber))
        {
            playerStats.setBonusPrize("COLLECTIBLE DUDE");
        }
        else if (calcPrizeByIndex(3, playerNumber))
        {
            playerStats.setBonusPrize("ANTIVIRUS PROFESSIONAL CLEANER");
        }else if (calcPrizeByIndex(4, playerNumber))
        {
            playerStats.setBonusPrize("COOKING IS NOT YOUR JOB");
        }else if (calcPrizeByIndex(5, playerNumber))
        {
            playerStats.setBonusPrize("MASTER CHEF OF THE UNIVERSE");
        }else if (calcPrizeByIndex(6, playerNumber))
        {
            playerStats.setBonusPrize("ELECTRICIAN");
        }else if (calcDeathPrize(playerNumber))
        {
            playerStats.setBonusPrize("COULD YOU NOT DIE?");
        }
        else
        {
            playerStats.setBonusPrize("WIN SOMETHING PLS");
        }
        
        if (isClient && playerMoveset.playerName == "Markus Nobel" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetMarkusNobleStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef, playerStats.cableManagement,playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Ken Nolo" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKenNoloStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef,playerStats.cableManagement, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Kam Brylla" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetKamBryllaStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef,playerStats.cableManagement, playerStats.bonusPrize);
        }else if (isClient && playerMoveset.playerName == "Raphael Nosun" && playerMoveset.hasAuthority)
        {
            playerController.CmdSetRaphaelNosunStats(playerStats.totalPoints, playerStats.death, playerStats.friendlyKill, playerStats.powerUp, playerStats.collectible, playerStats.antivirusKilled, playerStats.notOrdered, playerStats.greetChef,playerStats.cableManagement, playerStats.bonusPrize);
        }
        
        StartCoroutine(wait2());

        
    }
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(1f);
        showPlayers();
        calcMVP();

    }

    private bool calcPrizeByIndex(int index, int playerNumber)
    {
        if (playerMoveset.playerName == "Markus Nobel" && playerMoveset.hasAuthority)
        {
            if (MarkusNobelStats[index] != 0 && MarkusNobelStats[index] >=
                (MarkusNobelStats[index] + KenNoloStats[index] + KamBryllalStats[index] + RapahelNosunStats[index]) /playerNumber
            )
            {
                return true;
            }
            
        }else if (playerMoveset.playerName == "Ken Nolo" && playerMoveset.hasAuthority)
        {
            if (KenNoloStats[index] != 0 && KenNoloStats[index] >=
                (MarkusNobelStats[index] + KenNoloStats[index] + KamBryllalStats[index] + RapahelNosunStats[index]) /playerNumber
            )
            {
                return true;
            }
        }else if (playerMoveset.playerName == "Kam Brylla" && playerMoveset.hasAuthority)
        {
            if (KamBryllalStats[index] != 0 && KamBryllalStats[index] >=
                (MarkusNobelStats[index] + KenNoloStats[index] + KamBryllalStats[index] + RapahelNosunStats[index]) /playerNumber
            )
            {
                return true;
            }
            
        }else if (playerMoveset.playerName == "Raphael Nosun" && playerMoveset.hasAuthority)
        {
            if (RapahelNosunStats[index] != 0 && RapahelNosunStats[index] >=
                (MarkusNobelStats[index] + KenNoloStats[index] + KamBryllalStats[index] + RapahelNosunStats[index]) /playerNumber
            )
            {
                return true;
            }
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
        CheckValuesBox.SetActive(false);
        backImage.SetActive(true);
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

