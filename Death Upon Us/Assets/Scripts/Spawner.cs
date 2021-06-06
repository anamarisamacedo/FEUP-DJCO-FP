using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;

class Spawner : MonoBehaviour
{
    public GameObject[] objects;
    public float spawnTime = 6f;            // How long between each spawn.
    private Vector3 spawnPosition;
    public int maxNoEnemies;
    public float protectedRange;
    public float spawnRange ;
    public float freeSpace;
    private float maxHeight;

    // Use this for initialization
    void Start () 
    {
        maxHeight = Terrain.activeTerrain.terrainData.size.y;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }
    float SampleHeightWithRaycast(Vector3 pos)
    {
        RaycastHit hit;
        float y = 10;
        //Raycast down to terrain
        if (Physics.Raycast(pos, -Vector3.up, out hit))
        {
            //Get y position
            y = (float)(hit.point.y);
        }
        return y;
    }
    void Spawn ()
    {
        Vector3 playerPos = Camera.main.transform.position;

        Collider[] enemiesInRange = Physics.OverlapSphere(playerPos, spawnRange, LayerMask.GetMask("Monsters"));

        int noEnemiesSpawn = maxNoEnemies - enemiesInRange.Length;

        Collider[] objectsInRange;
        while (noEnemiesSpawn > 0){
            do{
                float range = UnityEngine.Random.Range(protectedRange, spawnRange);
                float angle = (float)(Math.PI * 2 * UnityEngine.Random.Range(0, 360) / 360);

                spawnPosition.x = playerPos.x + range * (float)Math.Cos(angle);
                spawnPosition.y = maxHeight + 1;
                spawnPosition.z = playerPos.z + range * (float)Math.Sin(angle);
                spawnPosition.y = (float)(SampleHeightWithRaycast(spawnPosition) + 0.5);

                objectsInRange = Physics.OverlapSphere(spawnPosition, freeSpace, LayerMask.GetMask("Objects"));
            }while (objectsInRange.Length != 0);
            Instantiate(objects[UnityEngine.Random.Range(0, objects.Length - 1)], spawnPosition, Quaternion.identity);

            noEnemiesSpawn--;
        }
    }
}