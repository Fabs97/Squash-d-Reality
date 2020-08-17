using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance{ get; private set; }

    void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(this);
    }
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioSource backgroundMusicSource;

    #region Audio Clips

    [SerializeField] private AudioClip[] playerSteps;

    #endregion

    private void playMainSourceSound(AudioClip clip){
        mainSource.PlayOneShot(clip);
    }

    public void playSteps(){
        AudioClip playerStepsRandomClip = playerSteps[Random.Range(0, playerSteps.Length)];
        playMainSourceSound(playerStepsRandomClip);
    }

    public bool mainSourceIsPlaying(){
        return mainSource.isPlaying;
    }
}