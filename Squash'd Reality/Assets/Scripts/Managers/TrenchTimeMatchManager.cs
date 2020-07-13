using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;

public class TrenchTimeMatchManager : MatchManager
{
    public bool matchTimeEnded = false;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isServer && _networkingManager.numPlayers == GameObject.FindGameObjectsWithTag("Player").Length)
        {
            gameReady = true;
        }
        if (gameReady && !matchStarting)
        {
            UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject.FindObjectOfType<TrenchTime>().setPlayersConnected(GameObject.FindGameObjectsWithTag("Player").Length);
            matchStarting = true;
            UIManager.GetComponent<UIManager>().StartMatch(4f);
            for (int i = 0; i < players.Length; i++)
            {
                for (int j = 0; j < players[i].transform.childCount; j++)
                {
                    if (players[i].transform.GetChild(j).tag.Equals("Weapon"))
                    {
                        GameObject oldWeapon = players[i].transform.GetChild(j).gameObject;
                        Weapon newWeapon = (Weapon) oldWeapon.AddComponent(typeof(Pistol));
                        oldWeapon.GetComponent<Shoot>().updateWeapon(newWeapon);
                        if (players[i].gameObject.GetComponent<DummyMoveset>().hasAuthority)
                        {
                            uiManager.setWeaponImage("Pistol");
                            uiManager.setWeaponActive(true);
                        }

                    }
                }
            }
            StartCoroutine(matchStart());
        }

        if (matchTimeEnded)
        {
            GameObject.FindObjectOfType<TrenchTime>().timeEnded = true;
            if (isServer)
            {
                matchTimeEnded = false;
 
            }
        }
        
    }

    protected override IEnumerator matchStart()
    {
        return base.matchStart();
    }

    protected override void showPlayerUI()
    {
        base.showPlayerUI();
        
    }

    public override void timeEnded()
    {
        if (isServer)
        {
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].GetComponent<Spawner>().StopSpawning();
            }
        }
        matchTimeEnded = true;

    }

    protected override IEnumerator resetChallenge()
    {
        yield return new WaitForSeconds(2f);
        GameObject.FindObjectOfType<TrenchTime>().endChallenge(false);
    }

    
}
