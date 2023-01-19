using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BottleSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] butelka;
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] SpawnValue[] testedPositions;
    [SerializeField] int spawnCount = 0; //zlicza ile mamy juz spawnow, poki co nigdzie nieuzywane
    [SerializeField] int nullSpawnCount = 0; //zlicza ile spawnow nie bylo wykonanych, poki co nigdzie nieuzywane
    [SerializeField] float freeSpaceRange = 2f;
    [SerializeField] float spawnRadius = 100f;
    public GameObject player;

    void Start()
    {
        testedPositions = new SpawnValue[spawnPositions.Length];
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            testedPositions[i] = SpawnValue.nothing;
        }
        StartCoroutine(SpawnBottle());
    }

    IEnumerator SpawnBottle()
    {
        yield return new WaitForSeconds(10);
        int arrayIndex;
        int counter = 0;

        Vector3 spawnPosition;
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPosition = spawnPositions[i].position;
            if (Vector3.Distance(player.transform.position, spawnPosition) < spawnRadius)
                testedPositions[i] = SpawnValue.inRange;

        }

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            if (Physics2D.OverlapCircle(spawnPositions[i].position, freeSpaceRange) == null)
            {
                if(testedPositions[i] == SpawnValue.inRange)
                {
                    testedPositions[i] = SpawnValue.spawning;
                    ++counter;
                }
                else
                {
                    testedPositions[i] = SpawnValue.alreadyTested;
                }
            }
            else
            {
                testedPositions[i] = SpawnValue.alreadyTested;
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
                    //sprawdzanie, jaki jest dystans od gracza by butelka nie pojawiala sie tuz obok plecow 
                    if(Physics2D.OverlapCircle(spawnPositions[arrayIndex].position, 1f) == null)
                    {
                        int butelkaIndex = Random.Range(0, butelka.Length);
                        Instantiate(butelka[butelkaIndex], spawnPositions[arrayIndex].position, Quaternion.identity);
                        break;
                    }
                }
            }
        }
        else
        {
            ++nullSpawnCount;
        }

        StartCoroutine(SpawnBottle());
    }
}
