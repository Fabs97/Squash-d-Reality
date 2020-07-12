using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private GameObject[] players;

    private float rotationSpeed = 3f;
    private float moveSpeed = 3f;

    private float life;

    private float BasicDamage = 6.7f;
    private float MediumDamage = 13.4f;
    private float HighDamage = 20f;

    private float distanceToKill = 1f;
    private bool isExploding = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        life = 20f;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length!= 0)
        {
            int playerIndex = nearbyPlayerIndex();
            float distance = Vector3.Distance(transform.position, players[playerIndex].transform.position);
            if (!isExploding && distance <= distanceToKill)
            {
                isExploding = true;
                StartCoroutine(killNearbyPlayers(1f));
            }
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

    IEnumerator killNearbyPlayers(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, players[i].transform.position);
            if (distance <= distanceToKill)
            {
                players[i].GetComponent<DummyMoveset>().TakeDamage(1);
            }
        }
        Destroy(this.gameObject);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //TODO: insert animation kill
            if (other.gameObject.name == "BulletPistol")
            {
                life -= BasicDamage;
            }else if (other.gameObject.name == "BulletShotgun")
            {
                life -= BasicDamage;
            } else if (other.gameObject.name == "BulletAssaultRifle")
            {
                life -= MediumDamage;
            }else if (other.gameObject.name == "BulletSniperRifle")
            {
                life -= HighDamage;
            }else if (other.gameObject.name == "BulletSMG")
            {
                life -= BasicDamage;
            }

            if (life <= 0f)
            {
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
