using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] challengeDoors;
    [SerializeField] private GameObject leaderboardDoor;
    // Start is called before the first frame update
    void Start()
    {
        setChallengeDoorsActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setChallengeDoorsActive(bool value)
    {
        foreach (var door in challengeDoors)
        {
            door.SetActive(value);
        }
    }
}
s