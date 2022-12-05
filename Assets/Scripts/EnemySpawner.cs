using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    //float TimeDelay = 5f;
    bool canRespawn = true;

    public GameObject[] spawnPositions;
    int spawnCount = 1;

    //GameObject player;

    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
        spawnPositions = new GameObject[5];
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
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
    /*
    bool checkFreeSpace(Vector3 place)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("player");
        if (gameObject != null)
        {
            if (place == gameObject.transform.position)
                return false;
            if (place.x < gameObject.transform.position.x - 3 || place.x < gameObject.transform.position.x + 3)
                if (place.x < gameObject.transform.position.y - 3 || place.x < gameObject.transform.position.y + 3)
                    return false;
        }
        return true;
    }*/
}
