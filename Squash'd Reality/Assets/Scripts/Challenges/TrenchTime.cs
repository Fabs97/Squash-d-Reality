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
            Debug.Log("ENTRO QUI");
            endChallenge(false);
        }
        
        
    }

    public void setPlayersConnected(int num)
    {
        playersAlive = num;
        Debug.Log("PLAYER VIVI: " + playersAlive);
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
