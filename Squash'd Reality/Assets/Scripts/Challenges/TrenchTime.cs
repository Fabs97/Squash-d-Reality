using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrenchTime : Challenge
{

    private bool matchStarted = false;

    private int playersAlive;

    public bool timeEnded = false;
    private bool matchEnded = false;
    private GameObject[] _spawners;
    private bool spawnerSetted = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spawnerSetted = false;
        playersAlive = 100;
        _spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnerSetted && _spawners.Length == 0)
        {
            _spawners = GameObject.FindGameObjectsWithTag("Spawner");
        }
        else if(!spawnerSetted)
        {
            spawnerSetted = true;
            setDifficulty();
        }
        if (matchStarted && (playersAlive == 0) )
        {
            endChallenge(false);
        }

        if (!matchEnded && playersAlive>=0 && timeEnded && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            endChallenge(true);
            matchEnded = true;
        }
        

    }

    public void setPlayersConnected(int num)
    {
        playersAlive = num;
        matchStarted = true;
    }

    public void setPlayerDead()
    {
        playersAlive--;
    }
    
    public override void endChallenge(bool successful){
        base.endChallenge(successful);
    }
    
    protected override void setDifficulty() {
        _spawners = GameObject.FindGameObjectsWithTag("Spawner");
        try {
            setMatch();
        } catch (Exception e) {
            // This try catch has been done because this setting must be done
            // server only, but this object does not need a Network Identity!
            // The thrown exception regarding the playersNames is correct as it
            // is only something required server-side
            Debug.LogWarning("CookingTime::setDifficulty - Catched Exception: " + e.StackTrace);
        }
        finally {
            base.setDifficulty();
        }
    }
    
    private void setMatch()
    {
        
        int numPlayer = _networkingManager.getPlayersNames().Count;
        foreach (var _spawner in _spawners)
        {
            
            if (_spawner.name == "EnemySpawnerRoom")
            {
                _spawner.GetComponent<Spawner>().setSpawningDelay(numPlayer*1.75f);

            }else if (_spawner.name == "EnemySpawnerFromBoxes")
            {
                _spawner.GetComponent<Spawner>().setSpawningDelay(numPlayer*1.2f);
            }
        }
    }
}
