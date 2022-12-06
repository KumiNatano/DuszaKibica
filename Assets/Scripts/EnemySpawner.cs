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
    int spawnCount = 0;
    public float sphereRadius = 3;

    public GameObject player;

    void Start()
    {
        //availableSpawnPositions.Length = spawnPositions.Length;
        //player = GameObject.FindGameObjectWithTag("player");
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
        Vector3 spawnPosition = spawnPositions[spawnCount].transform.position;
        if (Vector3.Distance(player.transform.position, spawnPosition) > 3f)
        {
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
        else
        {
            ++spawnCount;
            if (spawnCount >= 4) spawnCount = 0;
            spawnPosition = spawnPositions[spawnCount].transform.position;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
        ++spawnCount;
        if (spawnCount >= 4) spawnCount = 0;
        
    }
    /*
    bool checkFreeSpace(GameObject checkedPosition)
    {
        //ten fragment zawiesza cale Unity, uwazac na to
        //if (!Physics.CheckSphere(checkedPosition.transform.position, sphereRadius))
        //{
        //    return true;
        //}
        //return false; 
        
        if ((player.transform.position.x > checkedPosition.transform.position.x - 3 && player.transform.position.x < checkedPosition.transform.position.x + 3) ||
            (player.transform.position.y > checkedPosition.transform.position.y - 3 && player.transform.position.y < checkedPosition.transform.position.y + 3))
        {
            //Debug.Log("player is here!");
            return false;
        }
        return true;
    }*/
}
