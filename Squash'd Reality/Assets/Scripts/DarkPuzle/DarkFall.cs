using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFall : MonoBehaviour
{
    private DarkPuzzle _puzzle;

    void Start()
    {
        _puzzle = Object.FindObjectOfType<DarkPuzzle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "DeathZone")
        {
            _puzzle.endChallenge(false);
        }
    }

    
}
