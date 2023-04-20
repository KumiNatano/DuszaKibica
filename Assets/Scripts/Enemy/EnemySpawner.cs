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

    //[SerializeField] GameObject[] enemy; //juz nie wykorzystywane
    [SerializeField] GameObject[] enemySpawningList; //lista przeciwnikow, ktorzy maja sie pojawic w okreslonej kolejnosci
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] SpawnValue[] testedPositions;
    [SerializeField] int spawnCount = 0; //zlicza ile mamy juz spawnow
    [SerializeField] int nullSpawnCount = 0; //zlicza ile spawnow nie bylo wykonanych
    [SerializeField] float freeSpaceRange = 2f;
    [SerializeField] float spawnRadius = 30f;
    public GameObject player;
    public bool finishedSpawning; // uzywane przez skrypt AreaObjectivees
    private Vector3 boxPosition = new Vector3(0f, 2f, 0f);
    private Vector3 boxSize = new Vector3(2f, 1f, 2f);

    void Start()
    {
        finishedSpawning = false;
        testedPositions = new SpawnValue[spawnPositions.Length];
        for(int i = 0; i< spawnPositions.Length; i++)
        {
            testedPositions[i] = SpawnValue.nothing;
        }
        StartCoroutine(SpawnEnemy());
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Vector3 ghostPosition = spawnPositions[i].transform.position + new Vector3(0f, 1f, 0f);
            Vector3 ghostSize = boxSize;
            Gizmos.DrawWireCube(ghostPosition, ghostSize);
        }
    }
#endif

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(5);
        int arrayIndex;
        int counter = 0;

        // ETAP 1: Wyszukiwanie pól w otoczeniu gracza
        Vector3 spawnPosition;
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnPosition = spawnPositions[i].position;
            testedPositions[i] = SpawnValue.inRange;
        }
        // ETAP 2: sprawdzanie, czy na miejscu do spawnowania nie ma innych wrogów lub gracza
        for (int i=0; i< spawnPositions.Length; i++)
        {
            Collider[] results1 = Physics.OverlapBox(spawnPositions[i].position + boxPosition, boxSize);
            //Debug.Log(this.name + " " + i + ", Etap 2: " + results1.Length);
            if (results1.Length == 0)
            {
                if(testedPositions[i] == SpawnValue.inRange)
                {
                    testedPositions[i] = SpawnValue.spawning;
                    ++counter;
                }
                else 
                    testedPositions[i] = SpawnValue.alreadyTested;
            }
            else 
                testedPositions[i] = SpawnValue.alreadyTested;
        }
        //  ETAP 3: spawnowania przeciwnikow
        if (counter >= 1)
        {
            if (spawnCount < enemySpawningList.Length)
                for (int i = 0; i < testedPositions.Length * 2; i++)
                {
                    arrayIndex = Random.Range(0, testedPositions.Length);
                    if (testedPositions[arrayIndex] == SpawnValue.spawning)
                    {
                        Collider[] results2 = Physics.OverlapBox(spawnPositions[arrayIndex].position + boxPosition, boxSize);
                        //Debug.Log(this.name + " " + arrayIndex + ", Etap 3: " + results2.Length);
                        if (results2.Length == 0)
                        {
                            Instantiate(enemySpawningList[spawnCount], spawnPositions[arrayIndex].position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
                            ++spawnCount;
                            break;
                        }
                    }
                }
            else
                finishedSpawning = true;
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
