using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject WeaponMagazineButton;
    public TextMeshProUGUI WeaponMagazineText;

    //-----------------------------------TIMER VARIABLES----------------------------------------------
    public float seconds, minutes;
    [SerializeField] public float timeLeft;
    [SerializeField] private bool startTimer = false;
    private bool matchStarting = false;
    private float timeStarting = 5f;
    private bool speedSetted = false;
    private void Awake()
    {
        setAllElementsActive(false);

    }

    void Start()
    {        
        setAllElementsActive(false);
        speedSetted = false;
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
    public void StartCountdown(float countDowntime)
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
            if (!speedSetted && SceneManager.GetActiveScene().name == "TrenchTime" && timeLeft <= 20f)
            {
                speedSetted = true;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<Enemy>().timeSpeedMultiplier = 1.4f;
                }
            }
        }
        else
        {
            timeLeft = 0f;
            startTimer = false;
            GameObject.FindObjectOfType<MatchManager>().timeEnded();
        }
    }
    
    //SET gameobject active or not
    public void setTimerActive(bool value)
    {
        timerCounter.gameObject.SetActive(value);
        Clock.gameObject.SetActive(value);
    }

    public float getTimeLeft()
    {
        return timeLeft;
    }
    
    //---------------------------------------PLAYER-----------------------------------------------
    //SET player name
    public void setPlayerName(string playerName)
    {
        PlayerName_Text.text = playerName;        
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
    public void setPowerUpImage(string abilityName)
    {
        Texture2D myTexture = Resources.Load("Images/AbilityImages/" + abilityName) as Texture2D;
        PowerUp_Image.GetComponent<RawImage>().texture = myTexture;
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
        WeaponMagazineButton.SetActive(value);
    }

    public void setMagazineActive(bool value)
    {
        WeaponMagazineButton.SetActive(value);
    }

    public void setMagazineValue(string value)
    {
        WeaponMagazineText.text = "Magazine: " + value;
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
        setMagazineActive(value);
    }

    public void showUIPlayer(bool value)
    {
        setTimerActive(value);
        setPlayerElementsActive(value);
    }
}
