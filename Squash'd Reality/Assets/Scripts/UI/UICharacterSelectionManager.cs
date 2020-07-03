using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UICharacterSelectionManager : NetworkBehaviour
{
    //UI elements
    [SerializeField] private  Button Character1;
    [SerializeField] private Button Character2;
    [SerializeField] private Button Character3;
    [SerializeField] private Button Character4;
    [SerializeField] private GameObject CharacterAlreadyChoosen;


    private void Start()
    {
        showCharacterAlreadyChoosen();

    }

    private void Update()
    {
    }

    public void setCharacter1Active(bool value)
    {
    
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

    void showCharacterAlreadyChoosen()
    {
        StartCoroutine(countdownDisappereance());
    }

    IEnumerator countdownDisappereance()
    {
        yield return new WaitForSeconds(2f);
        CharacterAlreadyChoosen.SetActive(false);
    }
}
