using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioMenu : MonoBehaviour
{
    private AudioSource mainSource;
    [SerializeField] private AudioClip[] mainMenuAudio;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        mainSource = GetComponent<AudioSource>();

    }

    void Start()
    {
    }

    private void Update()
    {
      
        
    }
    
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            mainSource.PlayOneShot(mainMenuAudio[0]);

        }

        if (scene.name == "Lobby")
        {
            mainSource.Stop();
        }
    }
}
