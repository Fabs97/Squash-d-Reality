using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private bool randomizeSpawn;
    [SerializeField] private bool deleteFromListAfterSpawn;
    [SerializeField] private bool startSpawningFromTheBeginning; 
    [Range(0, 40)][SerializeField] private float firstSpawnDelay;
    [Range(0, 10)][SerializeField] private float spawningDelay;
    [SerializeField] private List<Vector3> maxCoordinates;
    [SerializeField] private List<Vector3> minCoordinates;
    public int objectsToSpawnCount = 999;
    
    private int _spawningIndex = -1;

    void Start() {
        if(startSpawningFromTheBeginning) startSpawning();
    }

    public void startSpawning(){
        if(isServer){
            StartCoroutine(spawningCoroutine());
        }
    }

    public void removeZone(int index){
        maxCoordinates.RemoveAt(index);
        minCoordinates.RemoveAt(index);
    }

    IEnumerator spawningCoroutine(){
        int objectsSpawnedCount = 0;
        yield return new WaitForSeconds(firstSpawnDelay);
        while(!deleteFromListAfterSpawn || prefabsToSpawn.Count > 0){
            
            if(randomizeSpawn) _spawningIndex = Random.Range(0, prefabsToSpawn.Count);
            else _spawningIndex++; 
            if(_spawningIndex == prefabsToSpawn.Count) break;

            int randomZoneIndex = Random.Range(0, maxCoordinates.Count);
            float randomX = Random.Range(minCoordinates[randomZoneIndex].x, maxCoordinates[randomZoneIndex].x);
            float randomY = Random.Range(minCoordinates[randomZoneIndex].y, maxCoordinates[randomZoneIndex].y);
            float randomZ = Random.Range(minCoordinates[randomZoneIndex].z, maxCoordinates[randomZoneIndex].z);

            Vector3 tr = new Vector3(randomX, randomY, randomZ);
            Quaternion q = Quaternion.identity;
            GameObject prefabToSpawn = prefabsToSpawn[_spawningIndex];
            GameObject go = Instantiate(prefabToSpawn, tr, q);

            NetworkServer.Spawn(go);
            if(deleteFromListAfterSpawn) prefabsToSpawn.Remove(prefabToSpawn);
            
            
            objectsSpawnedCount ++;
            if(objectsSpawnedCount == objectsToSpawnCount) break;
            yield return new WaitForSeconds(spawningDelay);
        }

        //destroy the spawner when this point is reached
        Destroy(gameObject);
    }
}
