using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemySpawnPoint[] enemySpawns;


    private void Awake()
    {
        main = this;
        enemySpawns = FindObjectsOfType<EnemySpawnPoint>();
    }
    IEnumerator Start()
    {
        Game.activeSpawn = defaultSpawn;
        yield return null;
        Game.activeSpawn.SpawnPlayer();
        RespawnEnemies();
    }
    public static void RespawnEnemies()
    {
        foreach(var spawn in main.enemySpawns)
        {
            spawn.Respawn();
        }
    }

    static GameManager main;
    [SerializeField] SpawnPoint defaultSpawn;
}
