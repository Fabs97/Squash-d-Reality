using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] challengeDoors;
    [SerializeField] private GameObject leaderboardDoor;

    private NetworkingManager.NetworkingManager _networkingManager;
    // Start is called before the first frame update
    void Start()
    {
        _networkingManager = FindObjectOfType<NetworkingManager.NetworkingManager>();
        List<string> playedRooms = _networkingManager.getPlayedRooms();
        foreach (var door in challengeDoors)
        {
            Debug.LogError("DOOR: " + door.GetComponent<Door>().nextSceneName);
            Debug.LogError("ACTIVE: " + playedRooms.Contains(door.GetComponent<Door>().nextSceneName));
            door.SetActive(playedRooms.Contains(door.GetComponent<Door>().nextSceneName));
        }
    }
}
