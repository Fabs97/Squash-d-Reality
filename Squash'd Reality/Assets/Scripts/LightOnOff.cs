using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{

    private GameObject player;
    private Light me;
    private float luminosity;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        me = GetComponent<Light>();
        luminosity = me.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        bool on = !player.GetComponent<Grabber>().GetIsGrabbing();

        if (on == false) me.intensity = 0;
        else me.intensity = luminosity;
        
    }
}
