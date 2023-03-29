using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaObjectives : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject borders;
    [SerializeField] GameObject[] blockade;

    // Update is called once per frame
    void Update()
    {
        if(enemySpawner.completeObjectives == true)
        {
            foreach(GameObject gameObject in blockade)
            {
                gameObject.SetActive(false);
            }
            borders.SetActive(false);
        }
    }
}
