using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartanArmorPowerUp : PowerUP
{
    protected override void Start()
    {
        base.Start();
    }

    public override void triggerEnter(Collider other)
    {
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        if (other.tag == "Player")
        {
            other.GetComponent<AudioManager>().playPowerUpSound();
            PlayerMoveset dm =  other.gameObject.GetComponent<PlayerMoveset>();
            dm.setSpartanArmorActive();
            if (dm.hasAuthority)
            {
                uiManager.setPowerUpImage("SpartanArmorPowerUp");
                uiManager.setPowerUpName("Spartan Armor");
                uiManager.setPowerUpButtonActive(true);
            }
            Destroy(gameObject);

        }
    }
}
