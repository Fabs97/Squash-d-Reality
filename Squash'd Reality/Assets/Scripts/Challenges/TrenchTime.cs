using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrenchTime : Challenge
{

    private bool matchStarted = false;

    private int playersAlive;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playersAlive = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (matchStarted && (playersAlive == 0) )
        {
            endChallenge(false);
        }

        if (playersAlive>=0 && GameObject.FindObjectOfType<TrenchTimeMatchManager>().matchTimeEnded && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            endChallenge(true);
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
}
