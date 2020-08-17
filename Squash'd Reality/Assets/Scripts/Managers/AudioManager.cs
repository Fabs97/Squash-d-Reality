using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : NetworkBehaviour
{
    private AudioSource mainSource;

    [SerializeField] private AudioClip[] footStep;
    [SerializeField] private AudioClip[] gunReload;
    [SerializeField] private AudioClip[] powerUp;

    void Start()
    {
        mainSource = GetComponent<AudioSource>();
    }


    public void playSound(int id)
    {
        if (id >= 0 && id <= footStep.Length)
        {
            CmdSendServerSoundIDFootstep(id);
        }
    }

    public void playSteps()
    {
        playSound(Random.Range(0, footStep.Length));
    }

    public void playOnlyClip()
    {
        playSound(0);
    }

    public void playGunSound()
    {
        CmdSendServerSoundIDGun(0);
    }

    public void playPowerUpSound()
    {
        CmdSendServerSoundIDPowerUp(0);
    }

    public bool isPlayingClip()
    {
        return mainSource.isPlaying;
    }

    [Command]
    public void CmdSendServerSoundIDFootstep(int id)
    {
        RpcSendSoundIDToClientFootstep(id);
    }
    
    [Command]
    public void CmdSendServerSoundIDGun(int id)
    {
        RpcSendSoundIDToClientGun(id);
    }

    [Command]
    public void CmdSendServerSoundIDPowerUp(int id)
    {
        RpcSendSoundIDToClientPowerUp(id);
    }

    
    [ClientRpc]
    public void RpcSendSoundIDToClientPowerUp(int id)
    {
        mainSource.PlayOneShot(powerUp[id]);
    }


    [ClientRpc]
    public void RpcSendSoundIDToClientFootstep(int id)
    {
        mainSource.PlayOneShot(footStep[id]);
    }
    [ClientRpc]
    public void RpcSendSoundIDToClientGun(int id)
    {
        mainSource.PlayOneShot(gunReload[id]);
    }

}