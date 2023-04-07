using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaObjectives : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject borders;
    [SerializeField] GameObject[] blockade;
    [SerializeField] GameObject enemies;
    [SerializeField] LevelTrigger levelTrigger;
    public bool activateArena;
    private bool isWorking = false;
    public bool goToNextArena = false;

    void Start()
    {
        activateArena = false;
        goToNextArena = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (activateArena)
        {
            if (!isWorking)
                startArena();
        }
        if (enemySpawner.finishedSpawning == true)
            completeArena();
    }

    public void startArena()
    {
        enemies.SetActive(true);
        borders.SetActive(true);
        enemySpawner.enabled = true;
        isWorking = true;
    }


    public void completeArena()
    {
        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            foreach (GameObject gameObject in blockade)
            {
                gameObject.SetActive(false);
            }
            borders.SetActive(false);
            isWorking = false;
            enemySpawner.StopAllCoroutines();
            goToNextArena = true;
        }
    }
}
