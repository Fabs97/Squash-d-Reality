using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    public List<GameObject> toSpawn;
    public bool isToShuffle;
    private IEnumerator coroutine;
    public float waitTime = 3f;
    private int number = 0;

    void Start()
    {
        if (isToShuffle)
        {
            Shuffle<GameObject>(toSpawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(number < toSpawn.Count)
        {
            Vector3 randomPos = new Vector3(Random.Range(0, 14), 10, Random.Range(0, 14));
            GameObject go = Instantiate(toSpawn[number], randomPos, Quaternion.identity);
            NetworkServer.Spawn(go);
            coroutine = WaitToSpawn(waitTime);
            StartCoroutine(coroutine);
        }
        
    }

    void Shuffle<GameObject>(List<GameObject> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, list.Count);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private IEnumerator WaitToSpawn(float wait)
    {
        yield return new WaitForSeconds(wait);
        number++;
    }
}
