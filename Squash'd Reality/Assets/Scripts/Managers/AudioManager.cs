using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : NetworkBehaviour {
    private AudioSource mainSource;

    [SerializeField] private AudioClip[] clips;
    
    void Start() {
        mainSource = GetComponent<AudioSource>();
    }


    public void playSound(int id){
        if( id >= 0 && id <= clips.Length){
            CmdSendServerSoundID(id);
        }
    }

    public void playSteps(){
        playSound(Random.Range(0, clips.Length));
    }

    public void playOnlyClip(){
        playSound(0);
    }

    public bool isPlayingClip(){
        return mainSource.isPlaying;
    }

    [Command]
    public void CmdSendServerSoundID(int id){
        RpcSendSoundIDToClient(id);
    }

    [ClientRpc]
    public void RpcSendSoundIDToClient(int id){
        mainSource.PlayOneShot(clips[id]);
    }
}