using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    [SerializeField] private List<GameObject> prefabsToSpawn;
    [SerializeField] private bool randomizeSpawn = false;
    [SerializeField] private bool deleteFromListAfterSpawn = false;
    [SerializeField] private bool startSpawningFromTheBeginning = false;
    [SerializeField] private bool spawnDoubleObject = false;
    [Range(0, 40)][SerializeField] public float firstSpawnDelay = 0f;
    [Range(0, 10)][SerializeField] public float spawningDelay = 0f;
    [SerializeField] private List<Vector3> maxCoordinates;
    [SerializeField] private List<Vector3> minCoordinates;
    public int objectsToSpawnCount = 999;
    
    private int _spawningIndex = -1;
    private int objectsSpawnedCount = 0;

    private float timeStopSpawn = 0;
    protected MatchManager _matchManager;



    private Coroutine spawnRoutine;

    [SerializeField] private bool isCookingTime;
    [SerializeField] private bool isElectroPipeline = false;

    void Start() {
        if(hasAuthority && startSpawningFromTheBeginning) CmdStartSpawning();
        _matchManager = FindObjectOfType<MatchManager>();

    }

    [Command]
    public void CmdStartSpawning(){
        if(isServer){
            spawnRoutine = StartCoroutine(spawningCoroutine());
        }
    }

    public void removeZone(int index){
        
        maxCoordinates.RemoveAt(index);
        minCoordinates.RemoveAt(index);
    }

    IEnumerator spawningCoroutine(){
        yield return new WaitForSeconds(firstSpawnDelay);
        while(!deleteFromListAfterSpawn || prefabsToSpawn.Count > 0){   
            if(_matchManager.getTimeLeft() <= timeStopSpawn) break;
            if(_spawningIndex == prefabsToSpawn.Count && !randomizeSpawn && !deleteFromListAfterSpawn) break;

            spawnObject(false);
            yield return new WaitForSeconds(.5f);
            if(spawnDoubleObject) spawnObject(true);            
            
            if(objectsSpawnedCount == objectsToSpawnCount) break;
            yield return new WaitForSeconds(spawningDelay);
        }

        //destroy the spawner when this point is reached
        Destroy(gameObject);
    }

    private void spawnObject(bool isDouble){
        if(randomizeSpawn) _spawningIndex = Random.Range(0, prefabsToSpawn.Count);
        else _spawningIndex++; 

        GameObject prefabToSpawn = prefabsToSpawn[_spawningIndex];
        
        int randomZoneIndex = Random.Range(0, maxCoordinates.Count);
        float randomX = Random.Range(minCoordinates[randomZoneIndex].x, maxCoordinates[randomZoneIndex].x);
        float randomY = Random.Range(minCoordinates[randomZoneIndex].y, maxCoordinates[randomZoneIndex].y);
        float randomZ = Random.Range(minCoordinates[randomZoneIndex].z, maxCoordinates[randomZoneIndex].z);
        if(isElectroPipeline){
            maxCoordinates.RemoveAt(randomZoneIndex);
            minCoordinates.RemoveAt(randomZoneIndex);
        }


        Vector3 tr = new Vector3(randomX, randomY, randomZ);
        Quaternion q = Quaternion.identity;
        if (isCookingTime)
        {
            if (!isDouble)
            {
                prefabToSpawn.GetComponent<Ingredient>().isDouble = false;
                GameObject go = Instantiate(prefabToSpawn, tr, q);
       
                NetworkServer.Spawn(go);
                if(deleteFromListAfterSpawn) prefabsToSpawn.Remove(prefabToSpawn);
                objectsSpawnedCount ++;
            }else if (isDouble && randomBool())
            {
                prefabToSpawn.GetComponent<Ingredient>().isDouble = true;
                GameObject go = Instantiate(prefabToSpawn, tr, q);
       
                NetworkServer.Spawn(go);
                if(deleteFromListAfterSpawn) prefabsToSpawn.Remove(prefabToSpawn);
            }  
        }
        else
        {
            GameObject go = Instantiate(prefabToSpawn, tr, q);
       
            NetworkServer.Spawn(go);
            if(deleteFromListAfterSpawn) prefabsToSpawn.Remove(prefabToSpawn);
            if (!isDouble)
            {
                objectsSpawnedCount ++;
            }
        }
    }

    public void StopSpawning() {
        try {
            StopCoroutine(spawnRoutine);
        }
        catch (System.Exception) {
            Debug.LogWarning("Spawner::StopSpawning -- Spawning routine had already stopped");
        }
    }

    public void setSpawningDelay(float delay){
        spawningDelay = delay;
    }

    public void setTimeStopSpawning(float timeStopSpawning)
    {
        timeStopSpawn = timeStopSpawning;
    }

    public bool randomBool()
    {
        return (Random.value > 0.5f);
    }
}
