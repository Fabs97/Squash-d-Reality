using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private GameObject[] players;
    private GameObject[] spawnPositions;

    private float rotationSpeed = 3f;
    private float moveSpeed = 0.5f;

    private float life;

    private float BasicDamage = 6.7f;
    private float MediumDamage = 13.4f;
    private float HighDamage = 20f;

    private float distanceToKill = 1f;
    private bool isExploding = false;
    private bool canFollowPlayer = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        life = 20f;
        players = GameObject.FindGameObjectsWithTag("Player");
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnDirection");

    }

    // Update is called once per frame
    void Update()
    {
        if (canFollowPlayer)
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
        else
        {
            int spawnIndex = nearbySpawnIndex();
            if (Vector3.Distance(transform.position,spawnPositions[spawnIndex].transform.position)>=0.3f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(spawnPositions[spawnIndex].transform.position-transform.position),rotationSpeed * Time.deltaTime );
                transform.position += transform.forward * moveSpeed * Time.deltaTime;    
            }
            else
            {
                canFollowPlayer = true;
            }
            

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

    private int nearbySpawnIndex()
    {
        int min_index = 0;
        float min_distance = 0f;
        min_distance = Vector3.Distance(transform.position, spawnPositions[0].transform.position);
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, spawnPositions[i].transform.position);
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
                players[i].GetComponent<PlayerMoveset>().TakeDamage(1);
            }
        }
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.LogError(other.gameObject.GetComponent<Bullet>().shooterName);
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
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in players)
                {
                    player.GetComponent<AudioManager>().playEnemyKilled();
                    PlayerMoveset playerMoveset = player.GetComponent<PlayerMoveset>();
                    if (playerMoveset.playerName ==
                        other.gameObject.GetComponent<Bullet>().shooterName)
                    {
                        playerMoveset.enemyKilled(); 
                    }
                }
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
