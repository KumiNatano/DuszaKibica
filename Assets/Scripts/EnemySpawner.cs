using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    //float TimeDelay = 5f;
    bool canRespawn = true;

    public GameObject[] spawnPositions;
    public GameObject[] availableSpawnPositions;
    int spawnCount = 0; //zlicza ile mamy juz spawnow, poki co nigdzie nieuzywane
    //public float sphereRadius = 3;

    public GameObject player;

    void Start()
    {
        availableSpawnPositions = new GameObject[5];
        //spawnPositions = new GameObject[5];
    }

    void Update()
    {
        if(canRespawn == true)
        {
            StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine()
    {
        canRespawn = false;

        yield return new WaitForSeconds(5);

        canRespawn = true;
        spawn();
    }

    void spawn()
    {
        availableSpawnPositions[0] = spawnPositions[0];
        addAvailableSpawnPositions();
        Vector3 spawnPosition;
        int enemyCounter = 0;
        foreach(GameObject spawn in availableSpawnPositions)
        {
            if (enemyCounter >= 1) //tutaj ustawiam ile za jednym razem moze maksymalnie zrespawnowac sie przeciwnikow
                break;

            if(spawn != null)
            {
                spawnPosition = spawn.transform.position;
                if (Vector3.Distance(player.transform.position, spawnPosition) > 3f)
                {
                    if (checkFreeSpace())
                    {
                        Instantiate(enemy, spawnPosition, Quaternion.identity);
                        ++spawnCount;
                        ++enemyCounter;
                    }
                }
            }
        }
        
        ++spawnCount;
        resetAvailableSpawnPositions();
    }

    bool checkFreeSpace()
    {
        float sphereRadius = 2;
        if (!Physics.CheckSphere(transform.position, sphereRadius))
        {
            return true;
        }
        return false;
    }

    void addAvailableSpawnPositions()
    {
        float sphereRadius = 10;
        int counter = 0;
        foreach(GameObject spawn in spawnPositions)
        {
            if(Vector3.Distance(player.transform.position, spawn.transform.position) < sphereRadius)
            {
                availableSpawnPositions[counter] = spawn;
                ++counter;
            }
        }
    }
    
    void resetAvailableSpawnPositions()
    {
        for(int i = 0; i < availableSpawnPositions.Length; i++)
        {
            availableSpawnPositions[i] = null;
        }
    }
}
