using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCollectible : MonoBehaviour
{
    [SerializeField] private int index;

    private void Update()
    {
        if(PlayerPrefs.GetString("Collectible_"+index, "false") == "false")
        {
            this.gameObject.GetComponent<Image>().color = new Color(255,0,0,255);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(255,255,255,255);
        }
    }
}
