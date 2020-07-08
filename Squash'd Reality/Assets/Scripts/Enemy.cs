using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private GameObject[] players;

    private float rotationSpeed = 3f;
    private float moveSpeed = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != 0)
        {
            int playerIndex = nearbyPlayerIndex();
            float distance = Vector3.Distance(transform.position, players[playerIndex].transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(players[playerIndex].transform.position-transform.position),rotationSpeed * Time.deltaTime );
            transform.position += transform.forward * moveSpeed * Time.deltaTime;   
        }
        
    }

    private int nearbyPlayerIndex()
    {
        int min_index = 0;
        float min_distance = 0f;
        min_distance = Vector3.Distance(transform.position, players[0].transform.position);
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, players[i].transform.position);
            if (distance < min_distance)
            {
                min_distance = distance;
                min_index = i;
            }
        }

        return min_index;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //TODO: insert animation kill
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
