using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioMenu : MonoBehaviour
{
    private AudioSource mainSource;
    [SerializeField] private AudioClip[] mainMenuAudio;

    private static AudioMenu _instance;

    public static AudioMenu Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        mainSource = GetComponent<AudioSource>();

    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            mainSource.Stop();
            mainSource.volume = 0.15f;
            mainSource.PlayOneShot(mainMenuAudio[0]);
            mainSource.loop = true;

        }

        if (scene.name == "Lobby")
        {
            mainSource.Stop();
        }
    }
}
