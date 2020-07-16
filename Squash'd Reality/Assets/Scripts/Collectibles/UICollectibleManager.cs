using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollectibleManager : MonoBehaviour
{
    [SerializeField] private GameObject WaitIntel;
    [SerializeField] private GameObject IntelAcquired;

    private void Start()
    {
        WaitIntel.SetActive(false);
        IntelAcquired.SetActive(false);
    }

    public void setWaitIntel(bool value)
    {
        WaitIntel.SetActive(value);
    }

    public void setIntelAcquired(bool value)
    {
        IntelAcquired.SetActive(value);
        if (value)
        {
            StartCoroutine(removeIntelAcquired());
        }
    }

    IEnumerator removeIntelAcquired()
    {
        yield return new WaitForSeconds(2f);
        IntelAcquired.SetActive(false);
    }
}
