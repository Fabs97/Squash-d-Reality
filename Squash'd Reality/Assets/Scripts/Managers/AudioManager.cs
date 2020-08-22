using System;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : NetworkBehaviour
{

    [SerializeField] private AudioClip[] footStep;
    [SerializeField] private AudioClip[] gunReload;
    [SerializeField] private AudioClip[] powerUp;
    [SerializeField] private AudioClip[] jump;
    [SerializeField] private AudioClip[] gunshot;
    [SerializeField] private AudioClip[] release;
    [SerializeField] private AudioClip[] winDie;
    [SerializeField] private AudioClip[] enemyExploded;
    [SerializeField] private AudioClip[] musicLevel;
    //MUSIC LEVEL INDEX
    /*
     * 0 --> cookingTime;
     * 1 --> darkPuzzle;
     * 2 --> trenchTime;
     * 3 --> electroPipeline;
     * 4 --> lobby
     */
    [SerializeField] private AudioClip[] enemyKilledSound;
    [SerializeField] private AudioClip[] collectibleSound;
    [SerializeField] private AudioSource enemyKilledSoundSource;
    [SerializeField] private AudioSource collectibleSoundSource;
    [SerializeField] private AudioSource footStepSource;
    [SerializeField] private AudioSource gunReloadSource;
    [SerializeField] private AudioSource powerUpSource;
    [SerializeField] private AudioSource jumpSource;
    [SerializeField] private AudioSource gunshotSource;
    [SerializeField] private AudioSource releaseSource;
    [SerializeField] private AudioSource winDieSource;
    [SerializeField] private AudioSource enemyExplodedSource;
    [SerializeField] private AudioSource musicLevelSource;

    private void Awake()
    {
        footStepSource = gameObject.AddComponent<AudioSource>();
        gunReloadSource = gameObject.AddComponent<AudioSource>();
        powerUpSource = gameObject.AddComponent<AudioSource>();
        jumpSource = gameObject.AddComponent<AudioSource>();
        gunshotSource = gameObject.AddComponent<AudioSource>();
        releaseSource = gameObject.AddComponent<AudioSource>();
        winDieSource = gameObject.AddComponent<AudioSource>();
        enemyExplodedSource = gameObject.AddComponent<AudioSource>();
        musicLevelSource = gameObject.AddComponent<AudioSource>();
        enemyKilledSoundSource = gameObject.AddComponent<AudioSource>();
        collectibleSoundSource = gameObject.AddComponent<AudioSource>();
    }

    public void playSound(int id)
    {
        if (id >= 0 && id <= footStep.Length && !footStepSource.isPlaying)
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
        if (hasAuthority)
        {
            gunReloadSource.PlayOneShot(gunReload[0]);
        }
        //CmdSendServerSoundIDGun(0);
    }

    public void playPowerUpSound()
    {
        if (hasAuthority)
        {
            powerUpSource.PlayOneShot(powerUp[0]);

        }
        //CmdSendServerSoundIDPowerUp(0);
    }

    public void playJumpSound()
    {
        CmdSendServerSoundIDJump(0);
    }

    public void playGunshotSound()
    {
        if (hasAuthority)
        {
            gunshotSource.PlayOneShot(gunshot[0]);
        }
    }

    public void playReleaseSound()
    {
        CmdSendServerSoundIDRelease(0);
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

    public void playEnemyExploded()
    {
        if (hasAuthority)
        {
            CmdSendServerEnemyExploded(0);
        }
    }

    [Command]
    public void CmdSendServerEnemyExploded(int id)
    {
        RpcSendSoundIDToClientEnemyExploded(id);
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
    public void RpcSendSoundIDToClientEnemyExploded(int id)
    {
        enemyExplodedSource.PlayOneShot(enemyExploded[id]);
        
    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientCollectible(int id)
    {
        collectibleSoundSource.PlayOneShot(collectibleSound[id]);
    }


    [ClientRpc]
    public void RpcSendSoundIDToClientEnemyKilled(int id)
    {
        if (hasAuthority)
        {
            enemyKilledSoundSource.PlayOneShot(enemyKilledSound[id]);
        }
    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientWinDie(int id)
    {
        winDieSource.PlayOneShot(winDie[id]);

    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientRelease(int id)
    {
        releaseSource.PlayOneShot(release[id]);

    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientGunshot(int id)
    {
        if (hasAuthority)
        {
            gunshotSource.PlayOneShot(gunshot[id]);
        }
    }
    [ClientRpc]
    public void RpcSendSoundIDToClientJump(int id)
    {
        jumpSource.PlayOneShot(jump[id]);
    }
    
    [ClientRpc]
    public void RpcSendSoundIDToClientPowerUp(int id)
    {
        if (hasAuthority)
        {
            powerUpSource.PlayOneShot(powerUp[id]);
        }
    }


    [ClientRpc]
    public void RpcSendSoundIDToClientFootstep(int id)
    {
        footStepSource.PlayOneShot(footStep[id]);
    }
    [ClientRpc]
    public void RpcSendSoundIDToClientGun(int id)
    {
        if (hasAuthority)
        {
            gunReloadSource.PlayOneShot(gunReload[id]);
        }
    }
    
    public void playMusicLevel(int id)
    {
        musicLevelSource.PlayOneShot(musicLevel[id]);
        musicLevelSource.loop = true;
        if (id == 4)
        {
            musicLevelSource.volume = 0.1f;
        }
        //musicLevelSource.volume = 0.1f;
        //CmdSendServerMusicLevel(id);
            
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
            musicLevelSource.PlayOneShot(musicLevel[id]);
            musicLevelSource.loop = true;
            musicLevelSource.volume = 0.1f;
        }
    }

}