using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private bool randomizeSpawn;
    [SerializeField] private bool deleteFromListAfterSpawn;
    [Range(0, 40)][SerializeField] private float firstSpawnDelay;
    [Range(0, 10)][SerializeField] private float spawningDelay;
    [SerializeField] private Vector3 maxCoordinates;
    [SerializeField] private Vector3 minCoordinates;
    
    private int _spawningIndex = 0;

    void Start() {
        StartCoroutine(spawningCoroutine());
    }

    IEnumerator spawningCoroutine(){
        yield return new WaitForSeconds(firstSpawnDelay);
        while(!deleteFromListAfterSpawn || prefabsToSpawn.Count > 0){
            
            if(randomizeSpawn) _spawningIndex = Random.Range(0, prefabsToSpawn.Count);

            float randomX = Random.Range(minCoordinates.x, maxCoordinates.x);
            float randomY = Random.Range(minCoordinates.y, maxCoordinates.y);
            float randomZ = Random.Range(minCoordinates.z, maxCoordinates.z);

            Vector3 tr = new Vector3(randomX, randomY, randomZ);
            Quaternion q = Quaternion.identity;
            GameObject prefabToSpawn = prefabsToSpawn[_spawningIndex];
            GameObject go = Instantiate(prefabToSpawn, tr, q);

            NetworkServer.Spawn(go);

            if(deleteFromListAfterSpawn) prefabsToSpawn.Remove(prefabToSpawn);
            yield return new WaitForSeconds(spawningDelay);
        }

        //destroy the spawner when this point is reached
        Destroy(gameObject);
    }
}
