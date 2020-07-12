using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverboardPowerUp : PowerUP
{
    public override void triggerEnter(Collider other)
    {
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if (other.tag == "Player")
        {
            DummyMoveset dm =  other.gameObject.GetComponent<DummyMoveset>();
            dm.setHoverboardActive();
            if (dm.hasAuthority)
            {
                uiManager.setPowerUpImage("HoverboardPowerUp");
                uiManager.setPowerUpName("Marty's Hoverboard");
                uiManager.setPowerUpButtonActive(true);
            }
            Destroy(gameObject);

        }
    }
}
