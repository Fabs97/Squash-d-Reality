using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update
    private GameObject dummyPrefab;
    private NetworkingManager.NetworkingManager _networkingManager;
    private LevelManager.LevelManager _levelManager;
    private GameObject bulletPrefab;

    private void Awake()
    {
       
    }

    void Start()
    {
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        _levelManager = FindObjectOfType<LevelManager.LevelManager>();
        if (isServer) {
            dummyPrefab = _networkingManager.spawnPrefabs[0];
        }
        
        if (isClient && isLocalPlayer)
        {
            gameObject.tag = "LocalPlayer";
            if(_levelManager.getCurrentLevel().spawnPlayers) CmdSpawnPlayer();
        }

        for (int i = 0; i < _networkingManager.spawnPrefabs.Count; i++)
        {
            if (_networkingManager.spawnPrefabs[i].tag.Equals("Bullet"))
            {
                bulletPrefab = _networkingManager.spawnPrefabs[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    [Command]
    public void CmdSelectedCharacter(string characterName)
    {
        dummyPrefab.name = characterName;

        UICharacterSelectionManager uICharacterSelectionManager = GameObject.Find("UICharacterSelectionManager").GetComponent<UICharacterSelectionManager>();
        if (characterName == "Character1") uICharacterSelectionManager.Character1Taken = true;
        else if (characterName == "Character2") uICharacterSelectionManager.Character2Taken = true;
        else if (characterName == "Character3") uICharacterSelectionManager.Character3Taken = true;
        else if (characterName == "Character4") uICharacterSelectionManager.Character4Taken = true;
        uICharacterSelectionManager.numCharactersChoosen++;
    }
    
    [Command]
    public void CmdSpawnPlayer() {
        GameObject go = Instantiate(dummyPrefab, dummyPrefab.transform);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
        Debug.Log("SpawnManager::CmdSpawnPlayer - Spawned my player!");
    }

    [Command]
    public void CmdAssignAuthority(GameObject gameObject)
    {
        NetworkServer.objects[gameObject.GetComponent<NetworkIdentity>().netId].AssignClientAuthority(connectionToClient);
    } 
    
    [Command]
    public void CmdRemoveAuthority(GameObject gameObject)
    {
        NetworkServer.objects[gameObject.GetComponent<NetworkIdentity>().netId].RemoveClientAuthority(connectionToClient);
    }

    [Command]
    public void CmdSpawnBullets(Vector3 position, Quaternion rotation, float spread, float bulletForce, string bulletName)
    {
        var randomNumberX = UnityEngine.Random.Range(-spread, spread);      
        var randomNumberY = UnityEngine.Random.Range(-spread, spread);     
        var randomNumberZ = UnityEngine.Random.Range(-spread, spread);
        GameObject spawnedGameObject = Instantiate(bulletPrefab, position, rotation);
        spawnedGameObject.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        spawnedGameObject.GetComponent<Rigidbody>().AddForce(spawnedGameObject.transform.forward * bulletForce, ForceMode.Impulse);
        spawnedGameObject.name = bulletName;
        NetworkServer.Spawn(spawnedGameObject);
        Destroy(spawnedGameObject, 3f);
    }
    
}
