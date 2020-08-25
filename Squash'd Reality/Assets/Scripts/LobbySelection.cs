using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySelection : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        UI.SetActive(true);
    }
}
