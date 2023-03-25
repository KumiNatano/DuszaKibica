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
            Collider[] results1 = Physics.OverlapBox(spawnPositions[i].position + new Vector3(0f, 2f, 0f), new Vector3(2f, 1f, 2f));
            //Debug.Log(i + " " + results1.Length + " AAA1");
            if (results1.Length == 0)
            //spawnPosition = spawnPositions[i].position;
            //if (Vector3.Distance(player.transform.position, spawnPosition) < spawnRadius)
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
                    Collider[] results2 = Physics.OverlapBox(spawnPositions[arrayIndex].position + new Vector3(0f, 2f, 0f), new Vector3(2f, 1f, 2f));
                    Debug.Log(arrayIndex + " "+results2.Length + " B_2");
                    if (results2.Length == 0)
                    //spawnPosition = spawnPositions[arrayIndex].position;
                    //if (Vector3.Distance(player.transform.position, spawnPosition) < spawnRadius)
                    {
                        int enemyIndex = Random.Range(0, enemy.Length);
                        Instantiate(enemy[enemyIndex], spawnPositions[arrayIndex].position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
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
