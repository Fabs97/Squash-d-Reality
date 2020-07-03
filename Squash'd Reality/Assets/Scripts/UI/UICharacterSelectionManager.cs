using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UICharacterSelectionManager : NetworkBehaviour
{
    //UI elements
    [SerializeField] private static Button Character1;
    [SerializeField] private Button Character2;
    [SerializeField] private Button Character3;
    [SerializeField] private Button Character4;

    //Networking variables
    [SyncVar] static private bool character1Taken = false;
    [SyncVar] private bool character2Taken = false;
    [SyncVar] private bool character3Taken = false;
    [SyncVar] private bool character4Taken = false;

    private void Update()
    {
        setCharacter1Active(!character1Taken);
        setCharacter2Active(!character2Taken);
        setCharacter3Active(!character3Taken);
        setCharacter4Active(!character4Taken);
    }

    public static void setCharacter1Active(bool value)
    {
        character1Taken = value;
        Character1.gameObject.SetActive(value);
    }
    
    void setCharacter2Active(bool value)
    {
        Character2.gameObject.SetActive(value); 
    }

    void setCharacter3Active(bool value)
    {
        Character3.gameObject.SetActive(value);
    }

    void setCharacter4Active(bool value)
    {
        Character4.gameObject.SetActive(value);
    }




}
