using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoStickPowerUp : PowerUP
{
    public override void triggerEnter(Collider other)
    {
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if (other.tag == "Player")
        {
            DummyMoveset dm =  other.gameObject.GetComponent<DummyMoveset>();
            dm.setPogoStickActive();
            if (dm.hasAuthority)
            {
                uiManager.setPowerUpImage("PogoStickPowerUp");
                uiManager.setPowerUpName("Pogo stick");
                uiManager.setPowerUpButtonActive(true);
            }
            Destroy(gameObject);

        }
    }
}
