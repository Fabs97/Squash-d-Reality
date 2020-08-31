using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Vista : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    private void Start()
    {
        Canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && other.GetComponent<PlayerMoveset>().hasAuthority)
        {
            Canvas.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerMoveset>().hasAuthority)
        {
            Canvas.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerMoveset>().hasAuthority)
        {
            Canvas.SetActive(false);
        }
    }
}
