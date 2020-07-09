using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private GameObject UIManager;
    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.FindWithTag("UIManager");
        UIManager.GetComponent<UIManager>().setInfoBoxText("Survive the enemies!");
        UIManager.GetComponent<UIManager>().setInfoBoxActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
