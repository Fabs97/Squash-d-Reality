using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Challenge : MonoBehaviour {
    protected int difficulty;
    protected NetworkingManager.NetworkingManager _networkingManager;
    protected LevelManager.LevelManager _levelManager;
    protected MatchManager _matchManager;
    [SerializeField] private GameObject[] doors;
    public bool matchIsWon;
    protected virtual void Start()
    {
        matchIsWon = false;
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        _levelManager = FindObjectOfType<LevelManager.LevelManager>();
        _matchManager = FindObjectOfType<MatchManager>();
    }

    protected virtual void setDifficulty() { }

    public virtual void endChallenge(bool successful){
        if (successful)
        {
            matchIsWon = true;
        }
        else
        {
            matchIsWon = false;
        }
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerMoveset>().hasAuthority)
            {
                if (successful)
                {
                    player.GetComponent<AudioManager>().playWinSound();

                }
                else
                {
                    player.GetComponent<AudioManager>().playDieSound();

                }
            }
        }
        if(successful){
            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<PlayerController>().CmdSetMatchWon();
            uiManager.setInfoBoxText("YOU WIN!");
            uiManager.setTimerActive(false);
            FindObjectOfType<NetworkingManager.NetworkingManager>().addPlayedRoom(SceneManager.GetActiveScene().name);
            StartCoroutine(waitToSpawnDoors());
        } else {
            uiManager.setInfoBoxText("YOU LOSE!");
            StartCoroutine(waitToReset());
        }
        uiManager.setInfoBoxActive(true);
    }

    IEnumerator waitToReset()
    {
        yield return new WaitForSeconds(2f);
        _networkingManager.serverChangeScene(_levelManager.getCurrentLevel().sceneName, difficulty);
    }

    IEnumerator waitToSpawnDoors()
    {
        yield return new WaitForSeconds(2f);
        UIManager uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        uiManager.setInfoBoxActive(false);
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(true);
        }
    }

}