using System.Collections;
using UnityEngine;

public class PlayerDeathEvent : MonoBehaviour
{
    public float preloadTime = 5f;
    void Start()
    {
        Player.main.living.onDeath += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine(Player.main));
    }
    IEnumerator PlayerDeathCoroutine(Player p)
    {
        // wyłączyć sterowanie

        yield return new WaitForSeconds(preloadTime);

        Game.activeSpawn.SpawnPlayer();
        GameManager.RespawnEnemies();
    }
}
