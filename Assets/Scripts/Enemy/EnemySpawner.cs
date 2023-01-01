using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnValue
{
    nothing = 0,
    alreadyTested = 1,
    inRange = 2,
    //isFree = 3,
    spawning = 4
}

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    //float TimeDelay = 5f;
    //bool canRespawn = true;

    [SerializeField] Transform[] spawnPositions;
    [SerializeField] SpawnValue[] testedPositions;
    [SerializeField] int spawnCount = 0; //zlicza ile mamy juz spawnow, poki co nigdzie nieuzywane
    [SerializeField] int nullSpawnCount = 0; //zlicza ile spawnow nie bylo wykonanych
    [SerializeField] float freeSpaceRange = 2f;
    [SerializeField] float spawnRadius = 100f;

    //public float sphereRadius = 3;

    public GameObject player;

    void Start()
    {
        testedPositions = new SpawnValue[spawnPositions.Length];
        for(int i = 0; i< spawnPositions.Length; i++)
        {
            testedPositions[i] = SpawnValue.nothing;
        }
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3);
        int arrayIndex;
        int counter = 0;

        Vector3 spawnPosition;
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPosition = spawnPositions[i].position;
            if(Vector3.Distance(player.transform.position, spawnPosition) < spawnRadius)
                testedPositions[i] = SpawnValue.inRange;

        }

        for (int i=0; i< spawnPositions.Length; i++)
        {
            //arrayIndex = Random.Range(0, spawnPositions.Length);
            if (Physics2D.OverlapCircle(spawnPositions[i].position, freeSpaceRange) == null)
            {
                if(testedPositions[i] == SpawnValue.inRange)
                {
                    testedPositions[i] = SpawnValue.spawning;
                    ++counter;
                    //break;
                }
                else
                {
                    testedPositions[i] = SpawnValue.alreadyTested;
                    //Debug.Log(i + "NIE OK");
                }
            }
            else
            {
                testedPositions[i] = SpawnValue.alreadyTested;
                //Debug.Log(i+ "NIE OK");
            }
        }
        //WriteSpawnsValues();

        if (counter >= 1)
        {
            for (int i = 0; i < testedPositions.Length*2; i++)
            {
                arrayIndex = Random.Range(0, testedPositions.Length);
                if (testedPositions[arrayIndex] == SpawnValue.spawning)
                {
                    //if( (sprawdzanie, jaki jest dystans od gracza by przeciwnik nie pojawial sie tuz obok plecow) )
                    if(Physics2D.OverlapCircle(spawnPositions[arrayIndex].position, 4f) == null)
                    {
                        Instantiate(enemy, spawnPositions[arrayIndex].position, Quaternion.identity);
                        //Debug.Log("Spawning from:" + arrayIndex + "OK");
                        break;
                    }
                }
            }
        }
        else
        {
            ++nullSpawnCount;
        }
        StartCoroutine(SpawnEnemy());
    }
    void WriteSpawnsValues()
    {
        Debug.Log("Tablica");
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if (testedPositions[i] == SpawnValue.spawning)
                Debug.Log(i + " = spawning");
            else if (testedPositions[i] == SpawnValue.inRange)
                Debug.Log(i + " = spawning");
            else if (testedPositions[i] == SpawnValue.alreadyTested)
                Debug.Log(i + " = spawning");
            else
                Debug.Log(i + " = alreadyTested");
        }
    }
}
