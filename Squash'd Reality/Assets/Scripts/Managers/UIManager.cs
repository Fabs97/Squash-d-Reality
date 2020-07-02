using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //-------------------------------------UI OBJECTS-------------------------------------------------

    public TextMeshProUGUI timerCounter;
    public RawImage PG_Image;
    public TextMeshProUGUI PlayerName_Text;
    public RawImage Weapon_Image;
    public RawImage PowerUp_Image;
    public TextMeshProUGUI PowerUp_Text;
    public TextMeshProUGUI IngredientsBox_Text;
    public TextMeshProUGUI InfoBox_Text;
    
    
    //-----------------------------------TIMER VARIABLES----------------------------------------------
    public float seconds, minutes;
    [SerializeField] private float timeLeft;
    [SerializeField] private bool startTimer = false;
    
    void Start()
    {
      
    }

    void Update()
    {
        
        //-----TIMER------
        if (startTimer)
        {
            Countdown();
        }

    }

    
    //--------------------------------------TIMER----------------------------------------------------
    //CALL this function to start timer
    void StartCountdown(float countDowntime)
    {
        timeLeft = countDowntime;
        startTimer = true;

    }
    //Timer logic
    void Countdown()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime; 
            minutes = (int) (timeLeft/ 60f); 
            seconds = (int) (timeLeft % 60f); 
            timerCounter.text = minutes.ToString("00") + ":" + seconds.ToString("00");   
        }
        else
        {
            timeLeft = 0f;
            startTimer = false;
            //TODO: settare tempo finito
        }
    }
    
    //---------------------------------------PLAYER-----------------------------------------------
    //SET player name
    public void setPlayerName(string playerName)
    {
        PlayerName_Text.text = playerName;
    }
    
    //SET player image
    public static void setPlayerImage()
    {
        //TODO
    }
    
    //---------------------------------------POWER-UP-----------------------------------------------
    //SET power-up name
    public void setPowerUpName(string powerUpName)
    {
        PowerUp_Text.text = powerUpName;
    }
    
    //SET power-up image
    public static void setPowerUpImage()
    {
        //TODO
    }
    //----------------------------------------WEAPON------------------------------------------------
    //SET weapon image
    public static void setWeaponImage()
    {
        //TODO
    }
    
    //---------------------------------------INFO BOX-----------------------------------------------
    //SET info box text
    public void setInfoBoxText(string infoBox)
    {
        InfoBox_Text.text = infoBox;
    }
    
    //--------------------------------------INGREDIENTS----------------------------------------------
    //SET info box text
    public void setIngredientsText(string ingredients)
    {
        IngredientsBox_Text.text = ingredients;
    }

}
