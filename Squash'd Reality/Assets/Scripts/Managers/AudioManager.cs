using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : NetworkBehaviour
{
    private AudioSource mainSource;

    [SerializeField] private AudioClip[] footStep;
    [SerializeField] private AudioClip[] gunReload;
    [SerializeField] private AudioClip[] powerUp;
    [SerializeField] private AudioClip[] jump;
    [SerializeField] private AudioClip[] gunshot;
    [SerializeField] private AudioClip[] release;
    [SerializeField] private AudioClip[] winDie;
    [SerializeField] private AudioClip[] musicLevel;
    [SerializeField] private AudioClip[] enemyKilledSound;
    [SerializeField] private AudioClip[] collectibleSound;
    
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

    public void playJumpSound()
    {
        CmdSendServerSoundIDJump(0);
    }

    public void playGunshotSound()
    {
        CmdSendServerSoundIDGunshot(0);
    }

    public void playReleaseSound()
    {
        CmdSendServerSoundIDRelease(0);
    }

    public bool isPlayingClip()
    {
        return mainSource.isPlaying;
    }

    public void playWinSound()
    {
        CmdSendServerSoundIDWinDie(0);
    }

    public void playDieSound()
    {
        CmdSendServerSoundIDWinDie(1);
    }

    public void playEnemyKilled()
    {
        CmdSendServerSoundEnemyKilled(0);
    }

    public void playCollectibleSound()
    {
        CmdSendServerCollectible(0);
    }


    [Command]
    public void CmdSendServerCollectible(int id)
    {
        RpcSendSoundIDToClientCollectible(id);
    }
    
    [Command]
    public void CmdSendServerSoundIDWinDie(int id)
    {
        RpcSendSoundIDToClientWinDie(id);
    }

    [Command]
    public void CmdSendServerSoundEnemyKilled(int id)
    {
        RpcSendSoundIDToClientEnemyKilled(id);
    }

    [Command]
    public void CmdSendServerSoundIDRelease(int id)
    {
        RpcSendSoundIDToClientRelease(id);
    }
    
    [Command]
    public void CmdSendServerSoundIDGunshot(int id)
    {
        RpcSendSoundIDToClientGunshot(id);
    }
    
    [Command]
    public void CmdSendServerSoundIDJump(int id)
    {
        RpcSendSoundIDToClientJump(0);
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
    public void RpcSendSoundIDToClientCollectible(int id)
    {
        mainSource.PlayOneShot(collectibleSound[id]);
    }


    [ClientRpc]
    public void RpcSendSoundIDToClientEnemyKilled(int id)
    {
        if (hasAuthority)
        {
            mainSource.PlayOneShot(enemyKilledSound[id]);
        }
    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientWinDie(int id)
    {
        mainSource.PlayOneShot(winDie[id]);

    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientRelease(int id)
    {
        mainSource.PlayOneShot(release[id]);

    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientGunshot(int id)
    {
        mainSource.PlayOneShot(gunshot[id]);

    }
    [ClientRpc]
    public void RpcSendSoundIDToClientJump(int id)
    {
        mainSource.PlayOneShot(jump[id]);
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
    
    public void playMusicLevel()
    {
        CmdSendServerMusicLevel(0);
            
    }

    [Command]
    public void CmdSendServerMusicLevel(int id)
    {
        RpcSendSoundIDToClientMusicLevel(id);
    }

    [ClientRpc]
    public void RpcSendSoundIDToClientMusicLevel(int id)
    {
        if (hasAuthority)
        {
            mainSource.PlayOneShot(musicLevel[id]);
            mainSource.volume = 0.1f;
        }
    }

}