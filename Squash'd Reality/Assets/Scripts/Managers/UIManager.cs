using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timerCounter;
    public float seconds, minutes;
    
    
    float timeLeft = 30.0f;
    void Start()
    {
        
    }

    void Update()
    { 
        timeLeft -= Time.deltaTime; 
        minutes = (int) (timeLeft/ 60f); 
        seconds = (int) (timeLeft % 60f); 
        timerCounter.text = minutes.ToString("00") + ":" + seconds.ToString("00");

    }

   

}
