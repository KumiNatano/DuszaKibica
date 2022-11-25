using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    float TimeDelay = 5f;
    bool canRespawn = true;

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
}
