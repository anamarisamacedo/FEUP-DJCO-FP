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
    private int maxNoEnemies = 10;
    private float protectedRange = 20;
    private float spawnRange = 100;
    private float freeSpace = 2;

    // Use this for initialization
    void Start () 
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    
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
                spawnPosition.y = 10;
                spawnPosition.z = playerPos.z + range * (float)Math.Sin(angle);

                objectsInRange = Physics.OverlapSphere(spawnPosition, freeSpace, LayerMask.GetMask("Objects"));
            }while (objectsInRange.Length != 0);
            Instantiate(objects[UnityEngine.Random.Range(0, objects.Length - 1)], spawnPosition, Quaternion.identity);

            noEnemiesSpawn--;
        }
    }
}