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

    [SerializeField] GameObject[] enemy;
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
            if (Physics.OverlapSphere(spawnPositions[i].position + new Vector3(0f, 3f, 0f), freeSpaceRange) == null)
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
                    //sprawdzanie, jaki jest dystans od gracza by przeciwnik nie pojawial sie tuz obok plecow) )
                    if(Physics.OverlapSphere(spawnPositions[arrayIndex].position + new Vector3(0f, 3f, 0f), 2f) == null)
                    {
                        int enemyIndex = Random.Range(0, enemy.Length);
                        Instantiate(enemy[enemyIndex], spawnPositions[arrayIndex].position, Quaternion.identity);
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
