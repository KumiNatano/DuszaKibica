using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AreaObjectives : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject borders;
    [SerializeField] GameObject[] blockade;
    [SerializeField] GameObject enemies;
    [SerializeField] LevelTrigger levelTrigger;
    public TargetController targetIndicatorEnemy;
    public GameObject player;
    public bool activateArena;
    private bool isWorking = false;
    public bool goToNextArena = false;
    private GameObject queueManager;

    void Start()
    {
        activateArena = false;
        goToNextArena = false;
        player = GameObject.FindGameObjectsWithTag("player")[0];
        queueManager = GameObject.FindWithTag("queueManager");
    }

    void Update()
    {
        if (activateArena)
        {
            if (!isWorking)
                startArena();
        }
        if (enemySpawner.finishedSpawning == true)
        {
            var enemies = FindObjectsByType<HealthSystem>(FindObjectsSortMode.None).Where(x => x.CompareTag("enemy") && x.enabled);
            if (enemies.Count(x => !x.GetComponent<AIPath>().enabled) == 0)
                completeArena();
        }     
    }

    public void startArena()
    {        
        enemies.SetActive(true);

        // Dodanie wskazników na wszystkich przeciwników.
        foreach (Transform enemy in enemies.transform) {
            TargetController newEnemyIndicator = Instantiate(targetIndicatorEnemy, player.transform);
            newEnemyIndicator.GetComponent<TargetController>().target = enemy;
        }
        
        queueManager.GetComponent<EnemyQueueManager>().scanLookingForEnemies();
        borders.SetActive(true);
        enemySpawner.enabled = true;
        isWorking = true;
    }

    public void completeArena()
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

    public Vector3 getTestArenaPosition()
    {
        return levelTrigger.transform.position;
    }

    public void setArenaIsCompleted()
    {
        enemySpawner.finishedSpawning = true;
        enemySpawner.endSpawningRightNow();
        completeArena();
    } 

    public Transform GetLevelTrigger()
    {
        return levelTrigger.transform;
    }
}
