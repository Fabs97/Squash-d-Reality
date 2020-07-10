﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //-------------------------------------UI OBJECTS-------------------------------------------------

    public TextMeshProUGUI timerCounter;
    public RawImage Clock;
    public RawImage PG_Image;
    public Button PlayerName_Button;
    public TextMeshProUGUI PlayerName_Text;
    public RawImage Weapon_Image;
    public RawImage PowerUp_Image;
    public TextMeshProUGUI PowerUp_Text;
    public Button PowerUp_Button;
    public Button InfoBox;
    public TextMeshProUGUI InfoBox_Text;
    public Button MatchStartingBox;
    public TextMeshProUGUI MatchStartingBox_Text;
    public GameObject UIpanel;

    //-----------------------------------TIMER VARIABLES----------------------------------------------
    public float seconds, minutes;
    [SerializeField] private float timeLeft;
    [SerializeField] private bool startTimer = false;
    private bool matchStarting = false;
    private float timeStarting = 5f;
    private void Awake()
    {
        setAllElementsActive(false);

    }

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

        if (matchStarting)
        {
            StartMatchCountdown();
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
    
    //SET gameobject active or not
    public void setTimerActive(bool value)
    {
        timerCounter.gameObject.SetActive(value);
        Clock.gameObject.SetActive(value);
    }

    
    //---------------------------------------PLAYER-----------------------------------------------
    //SET player name
    public void setPlayerName(string playerName)
    {
        if (playerName == "Character1")
        {
            PlayerName_Text.text = "Markus Nobel";
        }else if (playerName == "Character2")
        {
            PlayerName_Text.text = "Ken Nolo";
        }else if (playerName == "Character3")
        {
            PlayerName_Text.text = "Kam Brylla";
        }else if (playerName == "Character4")
        {
            PlayerName_Text.text = "Raphael Nosun";
        }
        
        
    }
    
    //SET player image
    public void setPlayerImage(string playerName)
    {
        Texture2D myTexture = Resources.Load("Images/PGImages/" + playerName) as Texture2D;
        PG_Image.GetComponent<RawImage>().texture = myTexture;
    }
    
    //SET gameobject active or not
    public void setPlayerElementsActive(bool value)
    {
       PlayerName_Button.gameObject.SetActive(value);
       PG_Image.gameObject.SetActive(value);
    }
    
    //---------------------------------------POWER-UP-----------------------------------------------
    //SET power-up name
    public void setPowerUpName(string powerUpName)
    {
        PowerUp_Text.text = powerUpName;
    }
    
    //SET power-up image
    public void setPowerUpImage()
    {
        //TODO
    }

    //SET gameobject active or not
    public void setPowerUpButtonActive(bool value)
    {
        PowerUp_Button.gameObject.SetActive(value);
        PowerUp_Image.gameObject.SetActive(value);
    }
    
    //----------------------------------------WEAPON------------------------------------------------
    //SET weapon image
    public void setWeaponImage(string weaponImage)
    {
        Texture2D myTexture = Resources.Load("Images/WeaponImages/" + weaponImage) as Texture2D;
        Weapon_Image.GetComponent<RawImage>().texture = myTexture;
    }
    
    //SET gameobject active or not
    public void setWeaponActive(bool value)
    {
        Weapon_Image.gameObject.SetActive(value);
    }
    
    //---------------------------------------INFO BOX-----------------------------------------------
    //SET info box text
    public void setInfoBoxText(string infoBox)
    {
        InfoBox_Text.text = infoBox;
    }
    
    //SET gameobject active or not
    public void setInfoBoxActive(bool value)
    {
        InfoBox.gameObject.SetActive(value);
    }
    
    //--------------------------------------MATCH STARTING---------------------------------------------

    public void StartMatch(float timeStarting)
    {
        setMatchStartingButtonActive(true);
        matchStarting = true;
    }

    void StartMatchCountdown()
    {
        if (timeStarting >= 0f)
        {
            timeStarting -= Time.deltaTime; 
            minutes = (int) (timeStarting/ 60f); 
            seconds = (int) (timeStarting % 60f); 
            MatchStartingBox_Text.text = "Match starting in: " + minutes.ToString("00") + ":" + seconds.ToString("00");   
        }
        else
        {
            timeStarting = 0f;
            matchStarting = false;
            setMatchStartingButtonActive(false);
        }
    }
    
    //SET gameobject active or not
    public void setMatchStartingButtonActive(bool value)
    {
        MatchStartingBox.gameObject.SetActive(value);
    }

    //----------------------------------------ALL UI------------------------------------------------
    //SET all UI active or not
    public void setAllElementsActive(bool value)
    {
        setTimerActive(value);
        setPlayerElementsActive(value);
        setPowerUpButtonActive(value);
        setWeaponActive(value);
        setInfoBoxActive(value);
        setMatchStartingButtonActive(value);
    }

    public void showUIPlayer(bool value)
    {
        setTimerActive(value);
        setPlayerElementsActive(value);
        
    }
}
