using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            if(_levelManager.getCurrentLevel().spawnPlayers) CmdSpawnPlayer(GameObject.FindGameObjectWithTag("DDOL").GetComponent<DDOL>().playerName);
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
        UICharacterSelectionManager uICharacterSelectionManager = GameObject.Find("UICharacterSelectionManager").GetComponent<UICharacterSelectionManager>();
        FindObjectOfType<NetworkingManager.NetworkingManager>().addSelectedPlayer(characterName);
        if (characterName == "Markus Nobel") uICharacterSelectionManager.Character1Taken = true;
        else if (characterName == "Ken Nolo") uICharacterSelectionManager.Character2Taken = true;
        else if (characterName == "Kam Brylla") uICharacterSelectionManager.Character3Taken = true;
        else if (characterName == "Raphael Nosun") uICharacterSelectionManager.Character4Taken = true;
        uICharacterSelectionManager.numCharactersChoosen++;
    }
    
    [Command]
    public void CmdSpawnPlayer(String playerName) {
        GameObject go = Instantiate(dummyPrefab, _levelManager.getCurrentLevel().getPlayerPosition(playerName), Quaternion.identity);
        go.GetComponent<PlayerMoveset>().playerName = playerName;
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
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
    public void CmdSpawnBullets(Vector3 position, Quaternion rotation, float spread, float bulletForce, string bulletName, string shooterName)
    {
        var randomNumberX = UnityEngine.Random.Range(-spread, spread);      
        var randomNumberY = UnityEngine.Random.Range(-spread, spread);     
        var randomNumberZ = UnityEngine.Random.Range(-spread, spread);
        GameObject spawnedGameObject = Instantiate(bulletPrefab, position, rotation);
        spawnedGameObject.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        spawnedGameObject.GetComponent<Rigidbody>().AddForce(spawnedGameObject.transform.forward * bulletForce, ForceMode.Impulse);
        spawnedGameObject.name = bulletName;
        spawnedGameObject.GetComponent<Bullet>().shooterName = shooterName;
        NetworkServer.Spawn(spawnedGameObject);
        Destroy(spawnedGameObject, 3f);
    }

    [Command]
    public void CmdsetLight1(bool value)
    {
        GameObject.FindGameObjectWithTag("DarkPuzzleMatchManager").GetComponent<DarkPuzzleMatchManager>().light1On =
            value;
    }

    [Command]
    public void CmdsetLight2(bool value)
    {
        GameObject.FindGameObjectWithTag("DarkPuzzleMatchManager").GetComponent<DarkPuzzleMatchManager>().light2On =
            value;
    }
    
    [Command]
    public void CmdsetLight3(bool value)
    {
        GameObject.FindGameObjectWithTag("DarkPuzzleMatchManager").GetComponent<DarkPuzzleMatchManager>().light3On =
            value;
    }

    [Command]
    public void CmdsetLight4(bool value)
    {
        GameObject.FindGameObjectWithTag("DarkPuzzleMatchManager").GetComponent<DarkPuzzleMatchManager>().light4On =
            value;
    }

    [Command]
    public void CmdSetMarkusNobleStats(int points, int deaths, string bonusPrize)
    {
        UILeaderboard uiLeaderboard = GameObject.FindGameObjectWithTag("UILeaderboard").GetComponent<UILeaderboard>();
        uiLeaderboard.MarkusNobelEnabled = true;
        uiLeaderboard.MarkusNoblePoints = points;
        uiLeaderboard.MarkusNobleDeaths = deaths;
        uiLeaderboard.MarkusNobleBonusPrize = bonusPrize;
    }
    
    [Command]
    public void CmdSetKenNoloStats(int points, int deaths, string bonusPrize)
    {
        UILeaderboard uiLeaderboard = GameObject.FindGameObjectWithTag("UILeaderboard").GetComponent<UILeaderboard>();
        uiLeaderboard.KenNoloEnabled = true;
        uiLeaderboard.KenNoloPoints = points;
        uiLeaderboard.KenNoloDeaths = deaths;
        uiLeaderboard.KenNoloBonusPrize = bonusPrize;
    }
    
    [Command]
    public void CmdSetKamBryllaStats(int points, int deaths, string bonusPrize)
    {
        UILeaderboard uiLeaderboard = GameObject.FindGameObjectWithTag("UILeaderboard").GetComponent<UILeaderboard>();
        uiLeaderboard.KamBryllaEnabled = true;
        uiLeaderboard.KamBryllaPoints = points;
        uiLeaderboard.KamBryllaDeaths = deaths;
        uiLeaderboard.KamBryllaBonusPrize = bonusPrize;
    }
    
    [Command]
    public void CmdSetRaphaelNosunStats(int points, int deaths, string bonusPrize)
    {
        UILeaderboard uiLeaderboard = GameObject.FindGameObjectWithTag("UILeaderboard").GetComponent<UILeaderboard>();
        uiLeaderboard.RaphaelNosunEnabled = true;
        uiLeaderboard.RaphaelNosunPoints = points;
        uiLeaderboard.RaphaelNosunDeaths = deaths;
        uiLeaderboard.RaphaelNosunBonusPrize = bonusPrize;
    }

    [Command]
    public void CmdSetTransformTo(GameObject go, Vector3 position, Quaternion rotation){
        go.transform.position = position;
        go.transform.rotation = rotation;
    }
    

    [Command]
    public void CmdSetMesh(GameObject go, bool value)
    {
        go.GetComponent<PlayerMoveset>().meshActive = value;
    }
}
